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
				{ data: 'Name' },
				{ data: 'LangId' },
				{ data: 'Status' },
			],
			columnDefs: [
				{
					targets: 0,
					title: 'Actions',
					orderable: false,
					render: function (data, type, full, meta) {
						 return '\<a onclick = "updateKayit(\'' + data + '\')" class="btn btn-sm btn-clean btn-icon" title="Edit details">\
								<i class="la la-edit"></i>\
							</a>\
							<a onclick = "ulkeSil(\'' + data + '\')" class="btn btn-sm btn-clean btn-icon" title="Delete">\
								<i class="la la-trash"></i>\
							</a>\ ';
					},

				}, {
					targets: 3,
					render: function (data, type, full, meta) {
						return dilne(data);
					},
				}, {
					targets: 4,
					render: function (data, type, full, meta) {
						if (data)
							return '<i class="flaticon2-checkmark text-success"></i>';
						return '<i class="flaticon2-delete text-danger"></i>';
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

$(document).on('click', '.btnSil', function (e) {
	var dataId = $(this).data('id');

	e.preventDefault();
	swal.fire({
		title: 'Emin misiniz?',
		text: "Seçilen Silinecektir!",
		icon: 'question',
		showCancelButton: true,
		confirmButtonColor: '#d33',
		confirmButtonText: 'Evet, sil!',
		cancelButtonText: "Vazgeç",
		cancelButtonColor: '#1ab394',
	}).then(function (result) {
		if (result.value) {

			ulkeSil(dataId);

			//JobOrderDelete(dataId);
		}
	});
});


var a;
function yeniKayit() {
	$('#exampleModalSizeLg').modal('show');
	$(':input', this).val('');
	a = 0;
}

var secilenDeger;

function updateKayit(pid) {


	var table = $('#kt_datatable').DataTable();

	secilenDeger = table.data().filter(x => x.Id == pid)[0];
	console.log(secilenDeger);
	$('#Id').val(secilenDeger.Id);
	$("#Name").val(secilenDeger.Name); 
	if (secilenDeger.Status == 1) {
		document.getElementById("status").checked = true;
	} else {
		document.getElementById("status").checked = false;
	}

	$('#exampleModalSizeLg').modal('show');

	a = 1;
}


function ulkeEkleGuncelle(pid) {

	if (a == 0) {
		$.ajax({
			data: {
				country: {
					'Name': $("#Name").val(), 
					'Status': $('#status').is(':checked') == true ? 1 : 0

				}
			},
			dataType: 'json',
			cache: false,
			type: "POST",
			url: hst2,
			success: function (data) {
				console.log(data.hata);
				if (data == 'true') {
					Swal.fire(
						'Başarılı',
						'Eklendi',
						'success'
					);
					$('#exampleModalSizeLg').modal('hide');
					$('#kt_datatable').DataTable().ajax.reload();
				}
				else {
					Swal.fire(
						'HATA',
						data,
						'error'
					)
				}


			},
			complete: function (data2) {

				$('#exampleModal').pleaseWait('stop');

			},
			error: function (data2) {

			}
		});
	} else {
		var table = $('#kt_datatable').DataTable();
		secilenDeger = table.data().filter(x => x.Id == pid)[0];
		$.ajax({
			data: {
				country: {
					'Id': $("#Id").val(),
					'Name': $("#Name").val(), 
					'Status': $('#status').is(':checked') == true ? 1 : 0

				}
			},
			dataType: 'json',
			cache: false,
			type: "POST",
			url: hst3,
			success: function (data) {
				console.log(data.hata);
				if (data == 'true') {
					Swal.fire(
						'Başarılı',
						'Eklendi',
						'success'
					);
					$('#exampleModalSizeLg').modal('hide');
					$('#kt_datatable').DataTable().ajax.reload();
				}
				else {
					Swal.fire(
						'HATA',
						data,
						'error'
					)
				}


			},
			complete: function (data2) {

				$('#exampleModal').pleaseWait('stop');

			},
			error: function (data2) {

			}
		});
	}
}




function ulkeSil(Id) {


	swal.fire({
		title: 'Emin misiniz?',
		text: "Seçilen Silinecektir!",
		icon: 'question',
		showCancelButton: true,
		confirmButtonColor: '#d33',
		confirmButtonText: 'Evet, sil!',
		cancelButtonText: "Vazgeç",
		cancelButtonColor: '#1ab394',
	}).then(function (result) {
		if (result.value) {
			$.ajax({
				type: "POST",
				url: hst4,
				dataType: "json",
				data: {
					"Id": Id
				},
				success: function () {

					Swal.fire(
						'Başarılı',
						'Silindi',
						'success'
					);
					$('#kt_datatable').DataTable().ajax.reload();
				},
				error: function () {
					Swal.fire(
						'HATA',
						'Silinemedi',

						'error'
					)
				}
			})
		}
	});


}