namespace Stockportfolio

open WebSharper
open WebSharper.JavaScript
open WebSharper.UI
open WebSharper.UI.Client
open WebSharper.UI.Templating
open WebSharper.Sitelets
open WebSharper.Charting
open type WebSharper.UI.ClientServer

type IndexTemplate = Template<"wwwroot/index.html", ClientLoad.FromDocument>
type EndPoint = 
    | [<EndPoint "/">] CashFlow
    | [<EndPoint "/stockPortfolio">] StockFlow

[<JavaScript>]
module Pages =
    open System

    let incomegroup, gain, totalG = Var.Create "", Var.Create 0.0, Var.Create 0.0
    let expensegroup, loss, totalE = Var.Create "", Var.Create 0.0, Var.Create 0.0
    let storage_i, storage_e = Var.Create ([], []), Var.Create ([], [])

    let addTransaction totalValue categoryValue amountValue (labels, data) =
        let newTotalValue = totalValue + amountValue
        let newLabels = labels @ [categoryValue]
        let newData = data @ [amountValue]
        (newLabels, newData, newTotalValue)

    let addIncome () =
        let (newLabels, newData, newTotalIncome) = addTransaction totalG.Value incomegroup.Value gain.Value storage_i.Value
        storage_i.Value <- (newLabels, newData)
        totalG.Value <- newTotalIncome
        incomegroup.Value <- ""
        gain.Value <- 0.0

    let addExpenses () =
        let (newLabels, newData, newTotalExpenses) = addTransaction totalE.Value expensegroup.Value loss.Value storage_e.Value
        storage_e.Value <- (newLabels, newData)
        totalE.Value <- newTotalExpenses
        expensegroup.Value <- ""
        loss.Value <- 0.0

    let balanceView = View.Map2 (fun income expense -> sprintf "%.2f" (income - expense)) totalG.View totalE.View
    let totalIncomeView = View.Map (sprintf "%.2f") totalG.View
    let totalExpensesView = View.Map (sprintf "%.2f") totalE.View

    let CashFlowPage() =
       IndexTemplate.MoneyTracking()
            .incomegroup(incomegroup)
            .gains(gain)
            .expensegroup(expensegroup)
            .losses(loss)
            .Income(totalIncomeView)
            .Expenses(totalExpensesView)
            .Balance(balanceView)
            .gainings(fun _ -> if not (String.IsNullOrWhiteSpace(incomegroup.Value)) && gain.Value <> 0.0 then addIncome())
            .losings(fun _ -> if not (String.IsNullOrWhiteSpace(expensegroup.Value)) && loss.Value <> 0.0 then addExpenses())
            .Doc()

    type Equity = {
        Name: string
        Amount: float
        Price: float
        mutable LatestPrice: float
    }

    let stockN, stockA, stockP  = Var.Create "", Var.Create 0.0, Var.Create 0.0
    let private random = Random()
    let private shufflePrice (price: float) =
        let randomFactor = (random.NextDouble() - 0.5) * 0.2
        let trendFactor = 1.0 + randomFactor
        let newPrice = price * trendFactor
        newPrice

    let EquityData = []

    let loader () =
        match JS.Window.LocalStorage.GetItem("stocks") with
        | null -> EquityData
        | data ->
            try
                let stock = Json.Deserialize<Equity list>(data)
                stock |> List.map (fun st -> { st with LatestPrice = shufflePrice st.Price })
            with
            | _ -> EquityData

    let saver (stock: Equity list) =
        let data = Json.Serialize stock
        JS.Window.LocalStorage.SetItem("stocks", data)

    let EquityModel = 
        let share = loader()
        let model = ListModel.Create (fun asset -> asset.Name) share
        model.View |> View.Sink (fun st -> saver (List.ofSeq st))
        model

    let capital (str: string) =
        if String.IsNullOrWhiteSpace(str) then str
        else str.[0].ToString().ToUpper() + str.Substring(1).ToLower()

    let priceupdate() =
        EquityModel.Iter(fun eq ->
            let newLastPrice = shufflePrice eq.Price
            EquityModel.UpdateBy (fun s -> if s.Name = eq.Name then Some { s with LatestPrice = newLastPrice } else None) eq.Name
        )

    let LatestInfo () =
        async {
            while true do
                do! Async.Sleep 5000
                priceupdate()
        }
        |> Async.Start

    let StockFlowPage() = 
        LatestInfo()
        let portfolio =
            EquityModel.View
            |> View.Map (Seq.sumBy (fun st -> st.LatestPrice * st.Amount)
            )
        let totalProfitAndLoss =
            EquityModel.View
            |> View.Map (Seq.sumBy (fun i -> (i.LatestPrice * i.Amount) - (i.Price * i.Amount))
        )

        let graphing () =
            let source = Event<float>()
            let chart = LiveChart.Line source.Publish
            Renderers.ChartJs.Render(chart, Window = 10)

            //let rnd = System.Random()
            //async {
            //    while true do
            //        do! Async.Sleep 500
            //        let d = rnd.NextDouble() * 100.
            //        source.Trigger d
            //}
            //|> Async.Start

        let Equityrenew (stockN: Var<string>, stockA: Var<float>, stockP: Var<float>) =
            if not <| String.IsNullOrWhiteSpace(stockN.Value) && stockA.Value > 0.0 && stockP.Value > 0.0 then
                let newStock = {
                    Name = capital stockN.Value
                    Amount = stockA.Value
                    Price = stockP.Value
                    LatestPrice = shufflePrice stockP.Value
                }
                EquityModel.Add newStock
                stockN.Value <- ""; stockA.Value <- 0.0; stockP.Value <- 0.0

        let color1 plusminus = if plusminus >= 0.0 then "rgb(0, 255, 0)" else "red"

        let percentpandl equitySum totalPandL =
            View.Map2 (fun asset profitAndLoss -> sprintf "%.2f" (profitAndLoss * 100.0 / asset)) equitySum totalPandL

        let color2 totalPandL =
            View.Map (fun profitAndLoss -> if profitAndLoss >= 0.0 then "rgb(0, 255, 0)" else "red") totalPandL

        let Equityvalues asset =
            let marketFloor = asset.Amount * asset.Price
            let assetValue = asset.LatestPrice * asset.Amount
            let profitAndLoss = assetValue - marketFloor
            let print value = sprintf "%.2f" value
            (marketFloor, assetValue, profitAndLoss, print)

        IndexTemplate.StockPortfolio()
            .portfolioTableBody(
                EquityModel.View.DocSeqCached(fun stock ->
                    let (costBasis, marketValue, profitAndLoss, print) = Equityvalues stock
                    IndexTemplate.assetList()
                        .assetName(stock.Name)
                        .assetQuantity(print stock.Amount)
                        .assetPrice(print stock.Price)
                        .assetCurrentPrice(print stock.LatestPrice)
                        .color(color1 profitAndLoss)
                        .assetPandL(print profitAndLoss)
                        .assetCostBasis(print costBasis)
                        .MarketValue(print marketValue)
                        .remove(fun _ -> EquityModel.RemoveByKey stock.Name)
                        .Doc()))
            .assetName(stockN)
            .assetQuantity(stockA)
            .assetPrice(stockP)
            .Graph(client(graphing()))
            .portfolioValue(portfolio |> View.Map (sprintf "%.2f$"))
            .portfolioPandL(totalProfitAndLoss |> View.Map (sprintf "%.2f$"))
            .portfolioPercent(percentpandl portfolio totalProfitAndLoss)
            .colorPL(color2 totalProfitAndLoss)
            .add(fun _ -> Equityrenew (stockN, stockA, stockP))
            .Doc()

[<JavaScript>]
module Client =
    let router = Router.Infer<EndPoint>()
    let currentPage = Router.InstallHash CashFlow router

    [<SPAEntryPoint>]
    let Main =
        let renderInnerPage (currentPage: Var<EndPoint>) =
            currentPage.View.Map (fun link ->
                match link with
                | CashFlow -> Pages.CashFlowPage()
                | StockFlow -> Pages.StockFlowPage()
            )
            |> Doc.EmbedView

        IndexTemplate()
            .Balances("/#/")
            .Equities("/#/stockPortfolio")
            .PageContent(renderInnerPage currentPage)
            .Bind()