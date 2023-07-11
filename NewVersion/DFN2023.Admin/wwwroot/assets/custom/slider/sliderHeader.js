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
				{ data: 'SliderName' },
				{ data: 'Status' },
			],
			columnDefs: [
				{
					targets: 0,
					title: 'Actions',
					orderable: false,
					render: function (data, type, full, meta) {
						return   '\<a onclick = "updateSliderHeader(\'' + data + '\')" class="btn btn-sm btn-clean btn-icon" title="Edit details">\
								<i class="la la-edit"></i>\
							</a>\
							<a onclick = "sliderHeaderSil(\'' + data + '\')" class="btn btn-sm btn-clean btn-icon" title="Delete">\
								<i class="la la-trash"></i>\
							</a>\ ';
					},
				},
				{
					targets: 3,
					render: function (data, type, full, meta) {
						if (data)
							return '<i class="flaticon2-checkmark text-success"></i>';
						else return '<i class="flaticon2-delete text-danger"></i>';
					},
				}
				
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




var secilenDeger;
var update = false;
function yeniKayit() {
	$('#exampleModalSizeLg').modal('show');
	$(':input', this).val('');
	update = false;
}




function updateSliderHeader(pid) {


	secilenDeger = $('#kt_datatable').DataTable().data().filter(x => x.Id == pid)[0];

	$('#Id').val(secilenDeger.Id);
	$("#Name").val(secilenDeger.SliderName);

	if (secilenDeger.Status == 1) {
		document.getElementById("status").checked = true;
	} else {
		document.getElementById("status").checked = false;
	}


	update = true;
	//var submitButton = document.getElementById('btnSubmit');
	//submitButton.setAttribute('onclick', 'satisNoktasiGuncelle()');

	$('#exampleModalSizeLg').modal('show');

}

					

function sliderHeaderKaydet(pid) {
	$('#exampleModalSizeLg').pleaseWait();
	var sliderHeader = {
		'Id': 0,
		'SliderName': $("#Name").val(),
		'Status': $('#status').is(':checked') == true ? 1 : 0,
	};
	var dtt;
	if (update) {

		secilenDeger.SliderName = sliderHeader.SliderName;
		secilenDeger.Status = sliderHeader.Status;
	

		dtt = { selectedSliderHeader: secilenDeger };
	} else {
		dtt = { selectedSliderHeader: sliderHeader };
	}
	console.log(dtt);
	$.ajax({
		data: dtt,
		dataType: 'json',
		cache: false,
		type: "POST",
		url: '/' + lngg + '/SliderHeader/createSliderHeader',
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

			$('#exampleModalSizeLg').pleaseWait('stop');

		},
		error: function (data2) {

		}
	});
}


function sliderHeaderSil(pid) {

	swal.fire({
		text: "Id= " + pid + "  Sliderı Silmek istiyor musunuz?",
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
				data: { selectedSliderHeader: { Id: pid } },
				dataType: 'json',
				cache: false,
				type: "POST",
				url: '/' + lngg + '/SliderHeader/deleteSliderHeader',
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

