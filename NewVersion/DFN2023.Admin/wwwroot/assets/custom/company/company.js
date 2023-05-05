"use strict";
var KTDatatablesDataSourceAjaxServer = function () {

    var initTable1 = function () {
        var table = $('#kt_datatable');

        // begin first table
        table.DataTable({
            responsive: false,
            searchDelay: 500,
            processing: true,
            serverSide: true,
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
                { data: 'Id' },
                { data: 'Id' },
                { data: 'OfficialName' },
                /*{ data: null },*/
                { data: 'OfficialCountryName' },
                { data: 'ShortDescription' },
                { data: 'DetailDescription' },
                { data: 'Status' },
                //{ data: 'LangId' },
            ],
            columnDefs: [
                {
                    targets: 0,
                    title: 'Actions',
                    orderable: false,
                    render: function (data, type, full, meta) {
                        return '<div style="display: flex;">' +

                            '<a href="javascript:;" class="btn btn-sm btn-clean btn-icon" onclick="kategoriDuzenle(' + data + ')"><i class="la la-edit"></i></a>' +
                            '<a href="javascript:;" class="btn btn-sm btn-clean btn-icon" onclick="kategoriSil(' + data + ')"><i class="la la-trash"></i></a>' +

                            '<a href="javascript:;" class="btn btn-sm btn-clean btn-icon" data-toggle="dropdown"><i class="la la-cog"></i></a>' +
                            '<div class="dropdown-menu dropdown-menu-sm dropdown-menu-right">' +
                            '<ul class="nav nav-hoverable flex-column">' +

                            '<li class="nav-item"><a onclick="urunResimleri(' + data + ')" class="nav-link" href="#"><i class="nav-icon la la-leaf"></i><span class="nav-text">Şirket Resimleri</span></a></li>' +


                            '</ul>' +
                            '</div>' +

                            '</div> ' +
                            ' ' +
                            ' ';
                    },
                },

                //{
                //    targets: 3,
                //    data: "numero",
                //    render: function (data, type, row, meta) {
                //        return "Data 1: " + row.data().OfficialCountryName + ". Data 2: " + row.data().OfficialCityName;
                //    }
                //},

                {
                    targets: 6,
                    render: function (data, type, full, meta) {
                        if (data)
                            return '<i class="flaticon2-checkmark text-success"></i>';
                        return '<i class="flaticon2-delete text-danger"></i>';
                    },
                },
                //{
                //	targets: 6,
                //	render: function (data, type, full, meta) {
                //		return dilne(data);
                //	},
                //},

            ],
        });
    };

    return {

        //main function to initiate the module
        init: function () {
            initTable1();
        },

    };

}();

jQuery(document).ready(function () {
    KTDatatablesDataSourceAjaxServer.init();
});


var update = false;

function kayitModalOpen() {

    $('#exampleModalSizeLg').modal('show');
    $(':input').val('');
    $("#p_Status").prop('checked', true);

    update = false;
}

var secilendeger;
function kategoriDuzenle(pid) {
    update = true;
    $('#exampleModalSizeLg').modal('show');
    secilendeger = $('#kt_datatable').DataTable().data().filter(x => x.Id == pid)[0];

    $('#p_BrandName').val(secilendeger.BrandName);
    $('#p_OfficialName').val(secilendeger.OfficialName);
    $('#p_ShortDescription').val(secilendeger.ShortDescription);
    $('#p_DetailDescription').val(secilendeger.DetailDescription);
    $('#p_TaxOffice').val(secilendeger.TaxOffice);
    $('#p_TaxNo').val(secilendeger.TaxNo);
    $('#p_CompanyTypeId').val(secilendeger.CompanyTypeId);
    $('#p_OfficialAddress').val(secilendeger.OfficialAddress);
    $('#p_OfficialCountryId').val(secilendeger.OfficialCountryId);
    $('#p_OfficialCityId').val(secilendeger.OfficialCityId);
    $('#p_OfficialCountyId').val(secilendeger.OfficialCountyId);
    $('#p_MapAddress').val(secilendeger.MapAddress);
    $('#p_MapCountryId').val(secilendeger.MapCountryId);
    $('#p_MapCityId').val(secilendeger.MapCityId);
    $('#p_MapCountyId').val(secilendeger.MapCountyId);
    $('#p_MapX').val(secilendeger.MapX);
    $('#p_MapY').val(secilendeger.MapY);
    $('#p_Email').val(secilendeger.Email);
    $('#p_Phone').val(secilendeger.Phone);
    $('#p_Mobile').val(secilendeger.Mobile);
    $('#p_YearFounded').val(secilendeger.YearFounded);
    $('#p_Logo').val(secilendeger.Logo);
    $('#p_Attachment').val(secilendeger.Attachment);
    $('#p_Facebook').val(secilendeger.facebook);
    $('#p_Instagram').val(secilendeger.Instagram);
    $('#p_Tiktok').val(secilendeger.Tiktok);
    $('#p_Youtube').val(secilendeger.Youtube);
    $('#p_Whatsapp').val(secilendeger.Whatsapp);
    $('#p_Website').val(secilendeger.Website);
    $('#p_AdminNotes').val(secilendeger.AdminNotes);

    $("#p_Status").prop('checked', secilendeger.Status == 1 ? true : false);

}



function seriKaydet() {

    $('#exampleModalSizeLg').pleaseWait();
    var product = {
        'BrandName': $('#p_BrandName').val(),
        'OfficialName': $('#p_OfficialName').val(),
        'ShortDescription': $('#p_ShortDescription').val(),
        'DetailDescription': $('#p_DetailDescription').val(),
        'TaxOffice': $('#p_TaxOffice').val(),
        'TaxNo': $('#p_TaxNo').val(),
        'CompanyTypeId': $('#p_CompanyTypeId').val(),
        'OfficialAddress': $('#p_OfficialAddress').val(),
        'OfficialCountryId': $('#p_OfficialCountryId').val(),
        'OfficialCityId': $('#p_OfficialCityId').val(),
        'OfficialCountyId': $('#p_OfficialCountyId').val(),
        'MapAddress': $('#p_MapAddress').val(),
        'MapCountryId': $('#p_MapCountryId').val(),
        'MapCityId': $('#p_MapCityId').val(),
        'MapCountyId': $('#p_MapCountyId').val(),
        'MapX': $('#p_MapX').val(),
        'MapY': $('#p_MapY').val(),
        'Email': $('#p_Email').val(),
        'Phone': $('#p_Phone').val(),
        'Mobile': $('#p_Mobile').val(),
        'YearFounded': $('#p_YearFounded').val(),
        'Logo': $('#p_Logo').val(),
        'Attachment': $('#p_Attachment').val(),
        'Facebook': $('#p_Facebook').val(),
        'Instagram': $('#p_Instagram').val(),
        'Tiktok': $('#p_Tiktok').val(),
        'Youtube': $('#p_Youtube').val(),
        'Whatsapp': $('#p_Whatsapp').val(),
        'Website': $('#p_Website').val(),
        'AdminNotes': $('#p_AdminNotes').val(),
        'Status': $('#p_Status').is(':checked') == true ? 1 : 0,


    };
    var dtt;
    if (update) {

        secilendeger.BrandName = product.BrandName ;
        secilendeger.OfficialName = product.OfficialName ;
        secilendeger.ShortDescription = product.ShortDescription ;
        secilendeger.DetailDescription = product.DetailDescription ;
        secilendeger.TaxOffice = product.TaxOffice ;
        secilendeger.TaxNo = product.TaxNo ;
        secilendeger.CompanyTypeId = product.CompanyTypeId ;
        secilendeger.OfficialAddress = product.OfficialAddress ;
        secilendeger.OfficialCountryId = product.OfficialCountryId ;
        secilendeger.OfficialCityId = product.OfficialCityId ;
        secilendeger.OfficialCountyId = product.OfficialCountyId ;
        secilendeger.MapAddress = product.MapAddress ;
        secilendeger.MapCountryId = product.MapCountryId ;
        secilendeger.MapCityId = product.MapCityId ;
        secilendeger.MapCountyId = product.MapCountyId ;
        secilendeger.MapX = product.MapX ;
        secilendeger.MapY = product.MapY ;
        secilendeger.Email = product.Email ;
        secilendeger.Phone = product.Phone ;
        secilendeger.Mobile = product.Mobile ;
        secilendeger.YearFounded = product.YearFounded ;
        secilendeger.Logo = product.Logo ;
        secilendeger.Attachment = product.Attachment ;
        secilendeger.facebook = product.facebook ;
        secilendeger.Instagram = product.Instagram ;
        ecilendeger.Tiktok = product.Tiktok ;
        secilendeger.Youtube = product.Youtube ;
        secilendeger.Whatsapp = product.Whatsapp ;
        secilendeger.Website = product.Website ;
        secilendeger.AdminNotes = product.AdminNotes ;
        secilendeger.Status = product.Status;


        dtt = { ct: secilendeger };
    } else {
        dtt = { ct: product };
    }


    $.ajax({
        data: dtt,
        dataType: 'json',
        cache: false,
        type: "POST",
        url: '/' + lngg + '/Company/CreatedCompany',
        success: function (data) {
            console.log(data.hata);
            if (!data.hata) {

                swal.fire({
                    text: data.mesaj,
                    icon: "success",
                    buttonsStyling: false,
                    confirmButtonText: "Ok",
                    customClass: {
                        confirmButton: "btn font-weight-bold btn-light-primary"
                    }
                }).then(function () {
                    $('#exampleModalSizeLg').modal('hide');
                    $('#kt_datatable').DataTable().ajax.reload();
                });
            }
            else {
                swal.fire({
                    text: data.mesaj,
                    icon: "error",
                    buttonsStyling: false,
                    confirmButtonText: "Ok",
                    customClass: {
                        confirmButton: "btn font-weight-bold btn-light-primary"
                    }
                }).then(function () {

                });
            }


        },
        complete: function (data2) {

            $('#exampleModalSizeLg').pleaseWait('stop');

        },
        error: function (data2) {

        }
    });
}



function kategoriSil(pid) {

    swal.fire({
        text: "Id= " + pid + "  şirketi Silmek istiyor musunuz?",
        icon: "warning",
        buttonsStyling: false,
        confirmButtonText: "Ok",
        showCloseButton: true,
        showCancelButton: true,
        customClass: {
            confirmButton: "btn font-weight-bold btn-light-primary",
            cancelButton: "btn font-weight-bold btn-light-warning"
        }
    }).then(function (data) {

        if (data.isConfirmed) {

            $('#kt_datatable').pleaseWait();
            $.ajax({
                data: { ct: { Id: pid } },
                dataType: 'json',
                cache: false,
                type: "POST",
                url: '/' + lngg + '/Company/DeleteCompany',
                success: function (data) {

                    if (!data.hata) {

                        swal.fire({
                            text: data.mesaj,
                            icon: "success",
                            buttonsStyling: false,
                            confirmButtonText: "Ok",
                            customClass: {
                                confirmButton: "btn font-weight-bold btn-light-primary"
                            }
                        }).then(function () {
                            $('#kt_datatable').DataTable().ajax.reload();
                        });
                    }
                    else {
                        swal.fire({
                            text: data.mesaj,
                            icon: "error",
                            buttonsStyling: false,
                            confirmButtonText: "Ok",
                            customClass: {
                                confirmButton: "btn font-weight-bold btn-light-primary"
                            }
                        }).then(function () {

                        });
                    }


                },
                complete: function (data2) {

                    $('#kt_datatable').pleaseWait('stop');

                },
                error: function (data2) {

                }
            });
        }
    });
}







/*----------------------------------Resim Ekle----------------------------------*/
var selectUrunId = 0;
function urunResimleri(pid) {
    $('#boatImageModal').modal('show');

    tumResimler(pid);
    selectUrunId = pid;

    /*document.getElementById('boatImageTypeSelect').Value = "0";*/
    //const $select = document.querySelector('#boatImageTypeSelect');
    //$select.querySelectorAll('option')[0].selected = 'selected';
}

var table1;
function tumResimler(pid) {
    $('#kt_datatableImage').DataTable().destroy();

    // begin first table
    table1 = $('#kt_datatableImage').DataTable({
        searching: false,
        processing: true,
        serverSide: true,
        lengthMenu: [200, 100, 50, 25],
        /*pagelength: 200,*/
        /*lengthMenu: true,*/
        /*lengthChange: true,*/
        /*paging: false,*/
        searching: true,
        ajax: {

            url: hst11,
            type: 'POST',
            contentType: "application/json",
            dataType: "json",
            data: function (d) {
                let additionalValues = [];
                //additionalValues[0] = $('#boatImageTypeSelect').val();
                ////additionalValues[1] = "Additional Parameters 2";
                //d.AdditionalValues = additionalValues;

                d.FilterId = pid;
                return JSON.stringify(d);
            }
        },
        columns: [
            { data: 'RowNum' },
            { data: 'Id' },
            { data: 'Id' },
            { data: 'Boat' },
            /*{ data: 'ImageType' },//3*/
            { data: 'Path' },//4
            { data: 'Desc' },
            { data: 'Status' },
            /*{ data: 'RowNum' },*/
        ],
        rowReorder: {
            dataSrc: 'RowNum'
            /*selector: 'tr'*/

        },
        columnDefs: [
            {
                //orderable: false,
                targets: 1,
                title: 'Actions',
                orderable: false,
                render: function (data, type, full, meta) {
                    return ' <a class="nav-link" href="#" onclick="urunResimDuzenle(' + data + ')"><i class="nav-icon la la-edit"></i> </a>  ' +
                        '	 <a class="nav-link" href="#" onclick="urunResimSil(' + data + ')"><i class="nav-icon la la-trash"></i> </a> ';
                },
            },
            //{
            //    targets: 4,
            //    render: function (data, type, full, meta) {
            //        if (data == 1) {
            //            return 'Üst Görsel';
            //        } else if (data == 2) {
            //            return 'İç / Dış Görsel';
            //        } else if (data == 3) {
            //            return 'Yerleşim Planı';
            //        } else if (data == 4) {
            //            return 'Üst Metin Yanı';
            //        } else if (data == 5) {
            //            return 'Ödül İkonu';
            //        } else if (data == 6) {
            //            return 'Üst Görsel Mobil';
            //        } else {
            //            return data;
            //        }

            //    },
            //},
            {
                targets: 4,
                render: function (data, type, full, meta) {
                    if (data != null)
                        return '<img class="h-100px rounded-sm" src="' + website + 'assets/boat/' + data + '" alt="....">';

                },
            },

            {
                targets: 6,
                render: function (data, type, full, meta) {
                    if (data == 1) {
                        return '<i class="flaticon2-checkmark text-success"></i>';
                    } else
                        return '<i class="flaticon2-delete text-danger"></i>';
                },
            },

        ],
    });

    //table1.on('row-reorder', function (e, diff, edit) {
    //    //var result = 'Sıralama başlangıcı: ' + edit.triggerRow.data()[0] + '<br>';
    //    var oldValues = '';
    //    var newValues = '';
    //    var IdValues = '';

    //    for (var i = 0, ien = diff.length; i < ien; i++) {
    //        var rowData = table1.row(diff[i].node).data();

    //        //result += table1[2] + ' nın yeni değeri ' +
    //        //    diff[i].newData + ' (öncesi ' + diff[i].oldData + ')<br>';

    //        oldValues += diff[i].oldData + '-';
    //        newValues += diff[i].newData + '-';
    //        IdValues += rowData.Id + '-';

    //        console.log(rowData);


    //    }

    //    var dtt = { Ids: IdValues, OldValues: oldValues, NewValues: newValues };
    //    $.ajax({

    //        data: dtt,
    //        dataType: 'json',
    //        cache: false,
    //        type: "POST",
    //        //url: '/' + lngg + '/StaticContent/Ordering',
    //        //url: '/' + @Model.language + '/StaticContent/Ordering',
    //        url: '/tr/Boat/ImageRowOrdering',
    //        success: function (data) {
    //            console.log(data.hata);
    //            if (!data.hata) {

    //                swal.fire({
    //                    text: data.mesaj,
    //                    icon: "success",
    //                    buttonsStyling: false,
    //                    confirmButtonText: "Ok",
    //                    customClass: {
    //                        confirmButton: "btn font-weight-bold btn-light-primary"
    //                    }
    //                }).then(function () {
    //                    //$('#exampleModalSizeLg').modal('hide');
    //                    $('#kt_datatableImage').DataTable().ajax.reload();
    //                });
    //            }
    //            else {
    //                swal.fire({
    //                    text: data.mesaj,
    //                    icon: "error",
    //                    buttonsStyling: false,
    //                    confirmButtonText: "Ok",
    //                    customClass: {
    //                        confirmButton: "btn font-weight-bold btn-light-primary"
    //                    }
    //                }).then(function () {

    //                });
    //            }


    //        },
    //        //complete: function (data2) {

    //        //    //$('#exampleModalSizeLg').pleaseWait('stop');

    //        //},
    //        //error: function (data2) {

    //        //}
    //    });


    //});

    //$('#kt_datatableImage tbody').on('click', 'tr', function () {
    //    $(this).toggleClass('selected');
    //});

    //$('#btn_deleteMultipleRows').click(function () {
    //    /*function deleteMultipleRows() {*/
    //    /*alert('lcome');*/
    //    /*alert(table1.rows('.selected').data().length + ' row(s) selected');*/
    //    if (table1.rows('.selected').data().length == 0) {
    //        alert('Lütfen silinecek sutünları seçiniz');
    //        return;
    //    }

    //    var selectedRows = [];
    //    for (var i = 0; i < table1.rows('.selected').data().length; i++) {
    //        /*console.log(table1.rows('.selected').data()[i]);*/
    //        selectedRows.push(table1.rows('.selected').data()[i]);
    //    }

    //    swal.fire({
    //        text: "seçili  resimleri silmek istiyor musunuz? ",
    //        icon: "warning",
    //        buttonsStyling: false,
    //        confirmButtonText: "Ok",
    //        showCloseButton: true,
    //        showCancelButton: true,
    //        customClass: {
    //            confirmButton: "btn font-weight-bold btn-light-primary",
    //            cancelButton: "btn font-weight-bold btn-light-warning"
    //        }
    //    }).then(function (data) {

    //        if (data.isConfirmed) {

    //            $('#kt_datatableImage').pleaseWait();
    //            $.ajax({
    //                data: { imgList: selectedRows },
    //                dataType: 'json',
    //                cache: false,
    //                type: "POST",
    //                url: hst26,
    //                success: function (data) {

    //                    if (!data.hata) {

    //                        swal.fire({
    //                            text: data.mesaj,
    //                            icon: "success",
    //                            buttonsStyling: false,
    //                            confirmButtonText: "Ok",
    //                            customClass: {
    //                                confirmButton: "btn font-weight-bold btn-light-primary"
    //                            }
    //                        }).then(function () {
    //                            $('#kt_datatableImage').DataTable().ajax.reload();
    //                        });
    //                    }
    //                    else {
    //                        swal.fire({
    //                            text: data.mesaj,
    //                            icon: "error",
    //                            buttonsStyling: false,
    //                            confirmButtonText: "Ok",
    //                            customClass: {
    //                                confirmButton: "btn font-weight-bold btn-light-primary"
    //                            }
    //                        }).then(function () {

    //                        });
    //                    }


    //                },
    //                complete: function (data2) {

    //                    $('#kt_datatableImage').pleaseWait('stop');

    //                },
    //                error: function (data2) {

    //                }
    //            });
    //        }
    //    });


    //});


}



function dataTableImageRefresh() {
    /*$('#kt_datatableImage').dataTable().fnFilter('');*/
    $('#kt_datatableImage').DataTable().ajax.reload();
}

var update3 = false;
var resim1 = "";


function resimEkleModalOpen() {
    $('#resimEkleModal').modal('show');
    resim1 = "";
    $(':input').val('');
    /*$("#r_ImageType").val(0).trigger('change');*/
    update3 = false;
    $('#p_Foto1 > .image-input-wrapper').css('background-image', 'url( )').trigger('change');
    /*$("#videoinput").hide();*/

}

var secilendeger2;

function urunResimDuzenle(pid) {

    update3 = true;
    $('#resimEkleModal').modal('show');
    secilendeger2 = $('#kt_datatableImage').DataTable().data().filter(x => x.Id == pid)[0];
    $('#p_Foto1 > .image-input-wrapper').css('background-image', 'url(' + website + '/assets/boat/' + secilendeger2.Path + ')').trigger('change');
    resim1 = secilendeger2.Path;
    /*$("#r_ImageType").val(secilendeger2.ImageType).trigger('change');*/
    $("#r_RowNum").val(secilendeger2.RowNum);
    $("#r_ImageDesc").val(secilendeger2.Desc);


    $("#r_Status").prop('checked', secilendeger2.Status == 1 ? true : false);


}

//$('#closeit').click(function () {
//    MultiImageDropzone.removeAllFiles(true);
//});


function urunResimKaydet() {

    $('#resimEkleModal').pleaseWait();


    var productImages = {
        'CompanyId': selectUrunId,
        'Path': resim1,
        /*'ImageType': $('#r_ImageType').val(),*/
        'RowNum': $('#r_RowNum').val(),
        'Desc': $('#r_ImageDesc').val(),
        'Status': $('#r_Status').is(':checked') == true ? 1 : 0,

    };
    var dtt;
    if (update3) {
        secilendeger2.Path = productImages.Path;
        /*secilendeger2.ImageType = productImages.ImageType;*/
        secilendeger2.RowNum = productImages.RowNum;
        secilendeger2.Desc = productImages.Desc;
        secilendeger2.Status = productImages.Status;

        dtt = { img: secilendeger2 };
    } else {
        dtt = { img: productImages };
    }

    $.ajax({
        data: dtt,
        dataType: 'json',
        cache: false,
        type: "POST",
        url: hst12,
        success: function (data) {

            if (!data.hata) {

                swal.fire({
                    text: data.mesaj,
                    icon: "success",
                    buttonsStyling: false,
                    confirmButtonText: "Ok",
                    customClass: {
                        confirmButton: "btn font-weight-bold btn-light-primary"
                    }
                }).then(function () {
                    $('#resimEkleModal').modal('hide');
                    $('#kt_datatable').DataTable().ajax.reload();
                    //tumResimler(selectUrunId);
                });
            }
            else {
                swal.fire({
                    text: data.mesaj,
                    icon: "error",
                    buttonsStyling: false,
                    confirmButtonText: "Ok",
                    customClass: {
                        confirmButton: "btn font-weight-bold btn-light-primary"
                    }
                }).then(function () {

                });
            }


        },
        complete: function (data2) {

            $('#resimEkleModal').pleaseWait('stop');

        },
        error: function (data2) {

        }
    });
}


function urunResimSil(pid) {

    swal.fire({
        text: "Id= " + pid + "  Görseli silmek istiyor musunuz?",
        icon: "warning",
        buttonsStyling: false,
        confirmButtonText: "Ok",
        showCloseButton: true,
        showCancelButton: true,
        customClass: {
            confirmButton: "btn font-weight-bold btn-light-primary",
            cancelButton: "btn font-weight-bold btn-light-warning"
        }
    }).then(function (data) {

        if (data.isConfirmed) {

            $('#kt_datatableImage').pleaseWait();
            $.ajax({
                data: { img: { Id: pid } },
                dataType: 'json',
                cache: false,
                type: "POST",
                url: hst13,
                success: function (data) {
                    if (!data.hata) {
                        swal.fire({
                            text: data.mesaj,
                            icon: "success",
                            buttonsStyling: false,
                            confirmButtonText: "Ok",
                            customClass: {
                                confirmButton: "btn font-weight-bold btn-light-primary"
                            }
                        }).then(function () {
                            tumResimler(selectUrunId);
                        });
                    }
                    else {
                        swal.fire({
                            text: data.mesaj,
                            icon: "error",
                            buttonsStyling: false,
                            confirmButtonText: "Ok",
                            customClass: {
                                confirmButton: "btn font-weight-bold btn-light-primary"
                            }
                        }).then(function () {

                        });
                    }


                },
                complete: function (data2) {

                    $('#kt_datatableImage').pleaseWait('stop');

                },
                error: function (data2) {

                }
            });
        }
    });
}



var avatar1 = new KTImageInput('p_Foto1');

avatar1.on('cancel', function (imageInput) {
    resim1 = "";
    swal.fire({
        title: 'Görsel İptal',
        type: 'success',
        buttonsStyling: false,
        confirmButtonText: 'OK',
        confirmButtonClass: 'btn btn-primary font-weight-bold'
    });
});

avatar1.on('change', function (imageInput) {
    resimYukle(1, imageInput, "p_anaSayfaFoto1");
});

avatar1.on('remove', function (imageInput) {
    resim1 = "";
    swal.fire({
        title: 'Görsel Silindi',
        type: 'error',
        buttonsStyling: false,
        confirmButtonText: 'OK',
        confirmButtonClass: 'btn btn-primary font-weight-bold'
    });
});



function resimYukle(hng, imageInput, ths) {

    $('#' + ths).pleaseWait();
    var data = imageInput.input.files[0];
    var formData = new FormData();
    formData.append("files", data);

    $.ajax({
        data: formData,
        processData: false,
        contentType: false,
        url: hst14,
        type: 'POST',
        success: function (data) {
            if (data != 'false') {
                if (hng == 1) {
                    resim1 = data;
                }
                swal.fire({
                    title: 'Görsel Yükleme Başarılı',
                    type: 'success',
                    buttonsStyling: false,
                    confirmButtonText: 'OK',
                    confirmButtonClass: 'btn btn-primary font-weight-bold'
                });
            } else {
                resim1 = "";
                swal.fire({
                    title: 'Görsel Yüklenemedi',
                    type: 'error',
                    buttonsStyling: false,
                    confirmButtonText: 'OK',
                    confirmButtonClass: 'btn btn-primary font-weight-bold'
                });
            }
        },
        complete: function (data2) {
            $('#' + ths).pleaseWait('stop');
        },
        error: function (data2) {

        }
    });
}