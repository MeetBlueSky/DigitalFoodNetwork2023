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
				{ data: 'Name' },
				{ data: 'Desc' },


			],
			columnDefs: [
				{
					targets: 0,
					title: 'Actions',
					orderable: false,
					render: function (data, type, full, meta) {
						return '<div class="dropdown dropdown-inline"> ' +
							'<a href="javascript:;" class="btn btn-sm btn-clean btn-icon" data-toggle="dropdown">  <i class="la la-cog"></i>    </a> ' +
							'<div class="dropdown-menu dropdown-menu-sm dropdown-menu-right"> ' +
							'	<ul class="nav nav-hoverable flex-column"> ' +
							'		<li class="nav-item"><a class="nav-link" href="#" onclick="staticContentGrupTempDuzenle(' + data + ')"><i class="nav-icon la la-edit"></i><span class="nav-text">Düzenle</span></a></li> ' +
							'		<li class="nav-item"><a class="nav-link" href="#" onclick="staticContentGrupTempSil(' + data + ')"><i class="nav-icon la la-trash"></i><span class="nav-text">Sil</span></a></li> ' +
							'	</ul> ' +
							'</div> ' +
							'</div> ';
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



var update = false;

function kayitModalOpen() {

	$('#exampleModalSizeLg').modal('show');
	$(':input').val('');
	$('#p_dil').val(lng).trigger('change');
	update = false;

}

var secilendeger;
function staticContentGrupTempDuzenle(pid) {
	update = true;
	$('#exampleModalSizeLg').modal('show');
	secilendeger = $('#kt_datatable').DataTable().data().filter(x => x.Id == pid)[0];

	$('#p_adi').val(secilendeger.Name);
	$('#p_Ack').val(secilendeger.Desc);



}



function staticContentGrupTempKaydet() {

	$('#exampleModalSizeLg').pleaseWait();
	var staticContentGrupTemp = {
		'Id': 0,
		'Name': $('#p_adi').val(),
		'Desc': $('#p_Ack').val(),

	};
	var dtt;
	if (update) {
		secilendeger.Name = staticContentGrupTemp.Name;
		secilendeger.Desc = staticContentGrupTemp.Desc;

		dtt = { pro: secilendeger };
	} else {
		dtt = { pro: staticContentGrupTemp };
	}


	$.ajax({
		data: dtt,
		dataType: 'json',
		cache: false,
		type: "POST",
		url: '/' + lngg + '/StaticContentGrupTemp/CreatedStaticContentGrupTemp',
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


function staticContentGrupTempSil(pid) {

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
				data: { pro: { Id: pid } },
				dataType: 'json',
				cache: false,
				type: "POST",
				url: '/' + lngg + '/StaticContentGrupTemp/DeleteStaticContentGrupTemp',
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




