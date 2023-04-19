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
            lengthMenu: [50, 10, 25, 100],
            oLanguage: {
                sSearch: 'Ara',
                sLengthMenu: "Göster _MENU_ adet",
            },
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
                { data: 'Image' },//2
                { data: 'Name' },
                { data: 'ParentId' },
                { data: 'ChildId' },
                { data: 'MenuFeatureId' },//5
                { data: 'ClickType' },//6
                { data: 'OpeningType' },//7
                { data: 'MenuLayer' },
                { data: 'MenuLayerCode' },
                { data: 'Link' },
                { data: 'RowNum' },
                { data: 'Status' },//12
                { data: 'LangId' },//13
            ],
            columnDefs: [
                {
                    targets: 0,
                    title: 'Actions',
                    orderable: false,
                    render: function (data, type, full, meta) {
                        return '<div class="dropdown dropdown-inline"> ' +
                            '<a href="javascript:;" class="btn btn-sm btn-clean btn-icon" onclick="menuDuzenle(' + data + ')" >  <i class="la la-edit"></i>    </a> ' +
                            '<a href="javascript:;" class="btn btn-sm btn-clean btn-icon" onclick="menuSil(' + data + ')" >  <i class="la la-trash"></i>    </a> ' +
                            '</div> ';
                    },
                },
                {
                    targets: 13,
                    render: function (data, type, full, meta) {
                        if (data == 1) {
                            return '<i class="flaticon2-checkmark text-success"></i>';
                        } else
                            return '<i class="flaticon2-delete text-danger"></i>';
                    },
                },
                {
                    targets: 2,
                    render: function (data, type, full, meta) {
                        if (data != null)
                            return '<img width="300px" class="h-100px rounded-sm" src="' + website + 'assets/menu/' + data + '" alt="....">';

                    },
                },
                {
                    targets: 7,
                    render: function (data, type, full, meta) {
                        if (data == 1) {
                            return 'Onclick';
                        } else if (data == 2) {
                            return 'Link';
                        }  else {
                            return "Seçilmemiş";
                        }

                    },
                },
                {
                    targets: 8,
                    render: function (data, type, full, meta) {
                        if (data == 1) {
                            return 'Yeni Sayfa';
                        } else if (data == 2) {
                            return 'Aynı Sayfa';
                        }  else {
                            return "Seçilmemiş";
                        }

                    },
                },
                {
                    targets: 14,
                    render: function (data, type, full, meta) {
                        return dilne(data);
                    },
                },
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



var resim1 = "";

var update = false;

function kayitModalOpen() {

    $('#exampleModalSizeLg').modal('show');
    $(':input').val('');
    $('#p_Image > .image-input-wrapper').css('background-image', 'url( )').trigger('change');
    $("#p_ParentId").val(0).trigger('change');
    $("#p_ChildId").val(0).trigger('change');
    $("#p_MenuFeatureId").val(0).trigger('change');
    $("#p_ClickType").val(0).trigger('change');
    $("#p_OpeningType").val(0).trigger('change');
    $("#p_Status").prop('checked', true);
    resim1 = "";
    update = false;
}


var secilendeger;
function menuDuzenle(pid) {

    update = true;
    $('#exampleModalSizeLg').modal('show');
    secilendeger = $('#kt_datatable').DataTable().data().filter(x => x.Id == pid)[0];
    $('#p_Name').val(secilendeger.Name);
    $("#p_ParentId").val(secilendeger.ParentId).trigger('change');
    $("#p_ChildId").val(secilendeger.ChildId).trigger('change');
    $('#p_MenuFeatureId').val(secilendeger.MenuFeatureId).trigger('change');
    $('#p_ClickType').val(secilendeger.ClickType).trigger('change');
    $('#p_OpeningType').val(secilendeger.OpeningType).trigger('change');
    $('#p_RowNum').val(secilendeger.RowNum);
    $('#p_MenuLayer').val(secilendeger.MenuLayer);
    $('#p_MenuLayerCode').val(secilendeger.MenuLayerCode);
    $('#p_Link').val(secilendeger.Link);
    console.log(secilendeger);
    $('#p_Image > .image-input-wrapper').css('background-image', 'url(' + website + 'assets/brands/' + secilendeger.Image + ')').trigger('change');
    resim1 = secilendeger.Image;
    $("#p_Status").prop('checked', secilendeger.Status == 1 ? true : false);

}

function menuKaydet() {

    $('#exampleModalSizeLg').pleaseWait();
    var menu = {
        'Name': $('#p_Name').val(),
        'MenuFeatureId': $('#p_MenuFeatureId').val(),
        'ParentId': $('#p_ParentId').val(),
        'ChildId': $('#p_ChildId').val(),
        'ClickType': $('#p_ClickType').val(),
        'OpeningType': $('#p_OpeningType').val(),
        'MenuLayer': $('#p_MenuLayer').val(),
        'MenuLayerCode': $('#p_MenuLayerCode').val(),
        'Link': $('#p_Link').val(),
        'RowNum': $('#p_RowNum').val(),
        'Status': $('#p_Status').is(':checked') == true ? 1 : 0,
        'Image': resim1,

    };

    var dtt;
    if (update) {
        secilendeger.Name = menu.Name;
        secilendeger.MenuFeatureId = menu.MenuFeatureId;
        secilendeger.ParentId = menu.ParentId;
        secilendeger.ChildId = menu.ChildId;
        secilendeger.ClickType = menu.ClickType;
        secilendeger.OpeningType = menu.OpeningType;
        secilendeger.MenuLayer = menu.MenuLayer;
        secilendeger.MenuLayerCode = menu.MenuLayerCode;
        secilendeger.Link = menu.Link;
        secilendeger.Image = menu.Image;
        secilendeger.RowNum = menu.RowNum;
        secilendeger.Status = menu.Status;

        dtt = { menu: secilendeger };
    } else {
        dtt = { menu: menu };
    }
    $.ajax({
        data: dtt,
        dataType: 'json',
        cache: false,
        type: "POST",
        url: hst2,
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

                    $('#exampleModalSizeLg').modal('hide');
                    $('#kt_datatable').DataTable().ajax.reload();


                });
            }
            else {
                Swal.fire(
                    'HATA',
                    data.mesaj,
                    'error'
                )
            }


        },
        complete: function (data2) {

            $('#exampleModalSizeLg').pleaseWait('stop');

        },
        error: function (data2) {

        }
    });





}
function menuSil(pid) {

    swal.fire({
        text: "Id= " + pid + "  numaralaralı menü kısmını silmek istiyor musunuz? ",
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
                data: { id: pid },
                dataType: 'json',
                cache: false,
                type: "POST",
                url: hst4,
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


var avatar1 = new KTImageInput('p_Image');

avatar1.on('cancel', function (imageInput) {
    resim1 = "";
    swal.fire({
        title: 'Resim İptal',
        type: 'success',
        buttonsStyling: false,
        confirmButtonText: 'OK',
        confirmButtonClass: 'btn btn-primary font-weight-bold'
    });
});

avatar1.on('change', function (imageInput) {
    resimYukle(1, imageInput, "p_Image");
});

avatar1.on('remove', function (imageInput) {
    resim1 = "";
    swal.fire({
        title: 'Resim Silindi',
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
        url: hst3,
        type: 'POST',
        success: function (data) {
            if (data != 'false') {
                if (hng == 1) {
                    resim1 = data;
                }

                swal.fire({
                    title: 'Resim Yükleme Başarılı',
                    type: 'success',
                    buttonsStyling: false,
                    confirmButtonText: 'OK',
                    confirmButtonClass: 'btn btn-primary font-weight-bold'
                });
            } else {
                resim1 = "";
                swal.fire({
                    title: 'Resim Yüklenemedi',
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



