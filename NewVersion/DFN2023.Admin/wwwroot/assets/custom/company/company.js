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
						return   '\<a onclick = "kategoriDuzenle(\'' + data + '\')" class="btn btn-sm btn-clean btn-icon" title="Edit details">\
								<i class="la la-edit"></i>\
							</a>\
							<a onclick = "kategoriSil(\'' + data + '\')" class="btn btn-sm btn-clean btn-icon" title="Delete">\
								<i class="la la-trash"></i>\
							</a>\ ';
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

function kayitModalOpen() {

	$('#exampleModalSizeLg').modal('show');
	$(':input').val('');
	$('#p_dil').val(lng).trigger('change');

	update = false;
}

var secilendeger;
function kategoriDuzenle(pid) {
	update = true;
	$('#exampleModalSizeLg').modal('show');
	secilendeger = $('#kt_datatable').DataTable().data().filter(x => x.Id == pid)[0];

	$('#p_Name').val(secilendeger.Name); 

	$("#p_durum").prop('checked', secilendeger.Status == 1 ? true : false);

}



function seriKaydet() {

	$('#exampleModalSizeLg').pleaseWait();
	var product = {
		'Name': $('#p_Name').val(), 

		'Status': $('#p_durum').is(':checked') == true ? 1 : 0,


	};
	var dtt;
	if (update) {

		secilendeger.Name = product.Name; 

		secilendeger.Status = product.Status;


		dtt = { cat: secilendeger };
	} else {
		dtt = { cat: product };
	}


	$.ajax({
		data: dtt,
		dataType: 'json',
		cache: false,
		type: "POST",
		url: '/' + lngg + '/Category/CreatedKategori',
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
				data: { cat: { Id: pid } },
				dataType: 'json',
				cache: false,
				type: "POST",
				url: '/' + lngg + '/Category/DeleteKategori',
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


