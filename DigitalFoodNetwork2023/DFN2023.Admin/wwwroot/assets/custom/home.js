"use strict";
var KTDatatablesDataSourceAjaxServer = function () {

    var initTable1 = function () {
        var table = $('#kt_datatable');

        // begin first table
        table.DataTable({
            responsive: false,
            searching: false, 
            processing: true,
            serverSide: true,
            lengthChange : false,
            info: false,

            ajax: {

                url: hst,
                type: 'POST',
                contentType: "application/json",
                dataType: "json",
                data: function (d) {

                    return JSON.stringify(d);
                }
            },

            columns: [
                { data: 'Date' },

                { data: 'UserName' },
                { data: 'IP' },                  
                
                                     
                
            ],
            columnDefs: [
                
                {
                    targets: 0,
                    render: function (data, type, full, meta) {
                        var date = new Date(data).toLocaleDateString();
                        return date;
                    },
                },
                
                


            ],


        });
    };

    return {

        //main function to initiate the module
        init: function () {
            initTable1();
            $('#kt_datatable_paginate').hide();
        },

    };

}();

var KTDatatablesSCPDataSourceAjaxServer = function () {

    var initTableSCP = function () {
        var tableSCP = $('#kt_datatableSCP');

        // begin first table
        tableSCP.DataTable({
            responsive: false,
            searching: false,
            processing: true,
            serverSide: true,
            lengthChange: false,
            info: false,

            ajax: {

                url: hst2,
                type: 'POST',
                contentType: "application/json",
                dataType: "json",
                data: function (d) {

                    return JSON.stringify(d);
                }
            },

            columns: [
                { data: 'ModifiedDate' },

                { data: 'Id' },
                { data: 'Grup' },
                { data: 'Temp' },
                { data: 'Title' },


            ],
            columnDefs: [

                {
                    targets: 0,
                    render: function (data, type, full, meta) {
                        var date = new Date(data).toLocaleDateString();
                        return date;
                    },
                },




            ],


        });
    };

    return {

        //main function to initiate the module
        init: function () {
            initTableSCP();
            $('#kt_datatableSCP_paginate').hide();
        },

    };

}();

var KTDatatablesSCGPDataSourceAjaxServer = function () {

    var initTableSCGP = function () {
        var tableSCGP = $('#kt_datatableSCGP');

        // begin first table
        tableSCGP.DataTable({
            responsive: false,
            searching: false,
            processing: true,
            serverSide: true,
            lengthChange: false,
            info: false,

            ajax: {

                url: hst3,
                type: 'POST',
                contentType: "application/json",
                dataType: "json",
                data: function (d) {

                    return JSON.stringify(d);
                }
            },

            columns: [
                { data: 'ModifiedDate' },

                { data: 'Id' },
                { data: 'GrupTemp' },
                { data: 'Title' },


            ],
            columnDefs: [

                {
                    targets: 0,
                    render: function (data, type, full, meta) {
                        var date = new Date(data).toLocaleDateString();
                        return date;
                    },
                },




            ],


        });
    };

    return {

        //main function to initiate the module
        init: function () {
            initTableSCGP();
            $('#kt_datatableSCGP_paginate').hide();
        },

    };

}();

var dz;
jQuery(document).ready(function () {
    KTDatatablesDataSourceAjaxServer.init();
    KTDatatablesSCPDataSourceAjaxServer.init();
    KTDatatablesSCGPDataSourceAjaxServer.init();
});

function updateTextView(_obj) {
    var num = getNumber(_obj.val());
    if (num == 0) {
        _obj.val('');
    } else {
        _obj.val(num.toLocaleString());

    }
}
function getNumber(_str) {
    var arr = _str.split('');
    var out = new Array();
    for (var cnt = 0; cnt < arr.length; cnt++) {
        if (isNaN(arr[cnt]) == false) {
            out.push(arr[cnt]);
        }
    }


    return Number(out.join(''));
}


function dataTableRefresh() {
    $('#kt_datatable').DataTable().ajax.reload();
}

function dataTableSCPRefresh() {
    $('#kt_datatableSCP').DataTable().ajax.reload();
}

function dataTableSCGPRefresh() {
    $('#kt_datatableSCGP').DataTable().ajax.reload();
}
