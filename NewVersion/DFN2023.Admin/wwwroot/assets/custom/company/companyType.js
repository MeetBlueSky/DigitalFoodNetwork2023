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
				{ data: 'TypeName' },
				{ data: 'Icon' },
				{ data: 'RowNum' },
				{ data: 'Status' },
			],
			columnDefs: [
				{
					targets: 0,
					title: 'Actions',
					orderable: false,
					render: function (data, type, full, meta) {
						return   '\<a onclick = "CompanyTypeDuzenle(\'' + data + '\')" class="btn btn-sm btn-clean btn-icon" title="Edit details">\
								<i class="la la-edit"></i>\
							</a>\
							<a onclick = "companyTypeSil(\'' + data + '\')" class="btn btn-sm btn-clean btn-icon" title="Delete">\
								<i class="la la-trash"></i>\
							</a>\ ';
					},
				},
				{
					targets: 3,
					render: function (data, type, full, meta) {
						if (data != null && data.length > 0)
							return '<img class="h-100px rounded-sm" src="' + website + '/assets/static/' + data + '" alt="....">';
						else { return ''; }
					},
				},

				{
					targets: 5,
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
var resim1 = "";

function kayitModalOpen() {

	$('#exampleModalSizeLg').modal('show');
	$(':input').val('');
	$("#p_Status").prop('checked', true);

	$('#p_Foto1 > .image-input-wrapper').css('background-image', 'url( )').trigger('change');
	resim1 = "";

	update = false;
}

var secilendeger;
function CompanyTypeDuzenle(pid) {
	update = true;
	$('#exampleModalSizeLg').modal('show');
	secilendeger = $('#kt_datatable').DataTable().data().filter(x => x.Id == pid)[0];

	$('#p_typeName').val(secilendeger.TypeName); 
	$('#p_RowNum').val(secilendeger.RowNum);
	$("#p_Status").prop('checked', secilendeger.Status == 1 ? true : false);

	$('#p_Foto1 > .image-input-wrapper').css('background-image', 'url(' + website + '/assets/static/' + secilendeger.Icon + ')').trigger('change');
	resim1 = secilendeger.Icon;

}



function companyTypeKaydet() {

	$('#exampleModalSizeLg').pleaseWait();
	var product = {
		'TypeName': $('#p_typeName').val(), 
		'RowNum': $('#p_RowNum').val(),
		'Status': $('#p_Status').is(':checked') == true ? 1 : 0,

		'Icon': resim1,


	};
	var dtt;
	if (update) {

		secilendeger.TypeName = product.TypeName; 
		secilendeger.Icon = product.Icon;
		secilendeger.RowNum = product.RowNum; 
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
		url: '/' + lngg + '/Company/CreatedCompanyType',
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



function companyTypeSil(pid) {

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
				url: '/' + lngg + '/Company/DeleteCompanyType',
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


var avatar1 = new KTImageInput('p_Foto1');

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
	resimYukle(1, imageInput, "p_Foto1");
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
		url: '/' + lngg + '/Company/UploderImage',
		type: 'POST',
		success: function (data) {
			if (data != 'false') {
				if (hng == 1) {
					resim1 = data;
				}
				//if (hng == 2) {
				//	resim2 = data;
				//} if (hng == 3) {
				//	resim3 = data;
				//} if (hng == 4) {
				//	resim4 = data;
				//} if (hng == 5) {
				//	resim5 = data;
				//}

				swal.fire({
					title: 'Resim Yükleme Başarılı',
					type: 'success',
					buttonsStyling: false,
					confirmButtonText: 'OK',
					confirmButtonClass: 'btn btn-primary font-weight-bold'
				});
			} else {
				resim1 = "";
				//resim2 = "";
				//resim3 = "";
				//resim4 = "";
				//resim5 = "";
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