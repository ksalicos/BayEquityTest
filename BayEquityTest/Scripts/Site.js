var officerData = new kendo.data.DataSource({
    transport: {
        read: {
            url: "/ajax/officers",
            type: "get",
            dataType: "json"
        }
    }
});

var pipelineSummary = kendo.observable({
    loaded: false,
    notloaded: true,
    OriginatedCount: 0,
    OriginatedAmount: 0,
    SubmittedCount: 0,
    SubmittedAmount: 0,
    FundedCount: 0,
    FundedAmount: 0,
    PurchasePct: 0
});
kendo.bind($("#rightpane"), pipelineSummary);

var pipelineData = new kendo.data.DataSource({
    data: []
});

function Load(id) {
    $.getJSON("/ajax/pipeline/" + id, function (result) {
        pipelineSummary.set("OriginatedCount", result.Summary.OriginatedCount);
        pipelineSummary.set("OriginatedAmount", result.Summary.OriginatedAmount);
        pipelineSummary.set("SubmittedCount", result.Summary.SubmittedCount);
        pipelineSummary.set("SubmittedAmount", result.Summary.SubmittedAmount);
        pipelineSummary.set("FundedCount", result.Summary.FundedCount);
        pipelineSummary.set("FundedAmount", result.Summary.FundedAmount);
        pipelineSummary.set("PurchasePct", result.Summary.PurchasePct);
        pipelineSummary.set("loaded", true);
        pipelineSummary.set("notloaded", false);
        pipelineData.data(result.Pipeline);
    });
}

$(document).ready(function () {
    $("#officerList").kendoListView({
        template: officerTemplate,
        dataSource: officerData
    });
    $("#grid").kendoGrid({
        sortable: true,
        pageable: {
            refresh: true,
            pageSizes: true,
            buttonCount: 5
        },
        columns: [
            {
                field: "LoanNumber",
                title: "Number"
            },
            {
                field: "BorrowerFirstName",
                title: "First"
            },
            {
                field: "BorrowerLastName",
                title: "Last"
            },
            {
                field: "Amount",
                title: "Amount",
                format: "{0:c}"
            },
            {
                field: "Purpose"
            }
        ],
        dataSource: pipelineData
    });
});