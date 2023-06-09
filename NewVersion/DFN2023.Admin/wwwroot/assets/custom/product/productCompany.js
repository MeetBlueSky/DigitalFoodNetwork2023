﻿"use strict";
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
				{ data: 'Name' },
				{ data: 'CompanyName' },
				{ data: 'ProductBaseName' },
				{ data: 'CategoryName' },
				{ data: 'Code' },
				{ data: 'Price' },
				{ data: 'Currency' },
				{ data: 'RowNum' },
				{ data: 'Status' },
				//{ data: 'LangId' },
			],
			columnDefs: [
				{
					targets: 0,
					title: 'Actions',
					orderable: false,
					render: function (data, type, full, meta) {
						return '\<a onclick = "productCompanyDuzenle(\'' + data + '\')" class="btn btn-sm btn-clean btn-icon" title="Edit details">\
								<i class="la la-edit"></i>\
							</a>\
							<a onclick = "productCompanySil(\'' + data + '\')" class="btn btn-sm btn-clean btn-icon" title="Delete">\
								<i class="la la-trash"></i>\
							</a>\ ';
					},
				},
				//{
				//	targets: 7,
				//	render: function (data, type, full, meta) {
				//		if (data != null && data.length > 0)
				//			return '<img class="h-100px rounded-sm" src="' + website + '/assets/static/' + data + '" alt="....">';
				//		else { return ''; }
				//	},
				//},
				{
					targets: 10,
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
/*var resim1 = "";*/
function kayitModalOpen() {

	$('#exampleModalSizeLg').modal('show');
	$(':input').val('');
	$("#p_Status").prop('checked', true);

	//$('#p_Foto1 > .image-input-wrapper').css('background-image', 'url( )').trigger('change');
	//resim1 = "";

	update = false;
}

var secilendeger;
function productCompanyDuzenle(pid) {
	update = true;
	$('#exampleModalSizeLg').modal('show');
	secilendeger = $('#kt_datatable').DataTable().data().filter(x => x.Id == pid)[0];

	$('#p_Name').val(secilendeger.Name);
	$('#p_CompanyId').val(secilendeger.CompanyId);
	$('#p_ProductBaseId').val(secilendeger.ProductBaseId);
	$('#p_CategoryId').val(secilendeger.CategoryId);
	$('#p_Code').val(secilendeger.Code);
	$('#p_Price').val(secilendeger.Price);
	$('#p_Currency').val(secilendeger.Currency);
	$('#p_RowNum').val(secilendeger.RowNum);
	$("#p_Status").prop('checked', secilendeger.Status == 1 ? true : false);

	//$('#p_Foto1 > .image-input-wrapper').css('background-image', 'url(' + website + '/assets/static/' + secilendeger.Image + ')').trigger('change');
	//resim1 = secilendeger.Image;

}


function productCompanyKaydet() {

	$('#exampleModalSizeLg').pleaseWait();
	var product = {
		'Name': $('#p_Name').val(),
		'ProductBaseId': $('#p_ProductBaseId').val(),
		'CompanyId': $('#p_CompanyId').val(),
		'CategoryId': $('#p_CategoryId').val(),
		'Code': $('#p_Code').val(),
		'Price': $('#p_Price').val(),
		'Currency': $('#p_Currency').val(),
		'RowNum': $('#p_RowNum').val(),
		'Status': $('#p_Status').is(':checked') == true ? 1 : 0,

		/*'Image': resim1,*/

	};
	var dtt;
	if (update) {

		secilendeger.Name = product.Name;
		secilendeger.ProductBaseId = product.ProductBaseId;
		secilendeger.CompanyId = product.CompanyId;
		secilendeger.CategoryId = product.CategoryId;
		secilendeger.Code = product.Code;
		secilendeger.Price = product.Price;
		secilendeger.Currency = product.Currency;
		secilendeger.RowNum = product.RowNum;
		secilendeger.Status = product.Status;
		/*secilendeger.Image = product.Image;*/

		dtt = { ct: secilendeger };
	} else {
		dtt = { ct: product };
	}


	$.ajax({
		data: dtt,
		dataType: 'json',
		cache: false,
		type: "POST",
		url: '/' + lngg + '/Product/CreatedProductCompany',
		success: function (data) {
			console.log(data.hata);
			if (data == 'true') {

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
					html: data.mesaj,
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



function productCompanySil(pid) {

	swal.fire({
		text: "Id= " + pid + "  seriyi Silmek istiyor musunuz?",
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
				url: '/' + lngg + '/Product/DeleteProductCompany',
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



//var avatar1 = new KTImageInput('p_Foto1');

//avatar1.on('cancel', function (imageInput) {
//	resim1 = "";
//	swal.fire({
//		title: 'Resim İptal',
//		type: 'success',
//		buttonsStyling: false,
//		confirmButtonText: 'OK',
//		confirmButtonClass: 'btn btn-primary font-weight-bold'
//	});
//});

//avatar1.on('change', function (imageInput) {
//	resimYukle(1, imageInput, "p_Foto1");
//});

//avatar1.on('remove', function (imageInput) {
//	resim1 = "";
//	swal.fire({
//		title: 'Resim Silindi',
//		type: 'error',
//		buttonsStyling: false,
//		confirmButtonText: 'OK',
//		confirmButtonClass: 'btn btn-primary font-weight-bold'
//	});
//});


//function resimYukle(hng, imageInput, ths) {

//	$('#' + ths).pleaseWait();
//	var data = imageInput.input.files[0];
//	var formData = new FormData();
//	formData.append("files", data);

//	$.ajax({
//		data: formData,
//		processData: false,
//		contentType: false,
//		url: '/' + lngg + '/Product/UploderImage',
//		type: 'POST',
//		success: function (data) {
//			if (data != 'false') {
//				if (hng == 1) {
//					resim1 = data;
//				}
//				//if (hng == 2) {
//				//	resim2 = data;
//				//} if (hng == 3) {
//				//	resim3 = data;
//				//} if (hng == 4) {
//				//	resim4 = data;
//				//} if (hng == 5) {
//				//	resim5 = data;
//				//}

//				swal.fire({
//					title: 'Resim Yükleme Başarılı',
//					type: 'success',
//					buttonsStyling: false,
//					confirmButtonText: 'OK',
//					confirmButtonClass: 'btn btn-primary font-weight-bold'
//				});
//			} else {
//				resim1 = "";
//				//resim2 = "";
//				//resim3 = "";
//				//resim4 = "";
//				//resim5 = "";
//				swal.fire({
//					title: 'Resim Yüklenemedi',
//					type: 'error',
//					buttonsStyling: false,
//					confirmButtonText: 'OK',
//					confirmButtonClass: 'btn btn-primary font-weight-bold'
//				});
//			}
//		},
//		complete: function (data2) {
//			$('#' + ths).pleaseWait('stop');
//		},
//		error: function (data2) {

//		}
//	});

//}
