var officerData = new kendo.data.DataSource({
    transport: {
        read: {
            url: "/ajax/officers",
            type: "get",
            dataType: "json"
        }
    }
});

var pipelineData = new kendo.data.DataSource({
    transport: {
        read: {
            url: function () { return "/ajax/pipeline/" + _current },
            type: "get",
            dataType: "json"
        }
    },
    schema: {
        data: function (r) { return r; },
        total: function (r) { return r.length; }
    }

});

var pipelineSummary = kendo.observable({
    loaded: false,
    notloaded: true,
    pipeData: new kendo.data.DataSource({
        transport: {
            read: {
                url: function () { return "/ajax/totals/" + _current },
                type: "get",
                dataType: "json"
            }
        }
    })
});
kendo.bind($("#rightpane"), pipelineSummary);

var _current = null;
function Load(id) {
    var addgrid = false;
    if (_current == null) {
        pipelineSummary.set("loaded", true);
        pipelineSummary.set("notloaded", false);
        addgrid = true;
        _current = id;
    } else {
        _current = id;
        pipelineData.read();
    }
    pipelineSummary.pipeData.read();
    if (addgrid) addGrid();
}

function addGrid() {
    $("#grid").kendoGrid({
        sortable: true,
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
}

$(document).ready(function () {
    $("#officerList").kendoListView({
        template: officerTemplate,
        dataSource: officerData
    });
});