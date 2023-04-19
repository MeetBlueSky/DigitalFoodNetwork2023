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
				{ data: 'Image1' },
				{ data: 'Image2' },
				{ data: 'Name' },
				{ data: 'Link' },
				{ data: 'Target' },
				{ data: 'Type' },
				{ data: 'RowNum' },
				{ data: 'Status' },
				{ data: 'LangId' }
			],
			columnDefs: [
				{
					targets: 0,
					title: 'Actions',
					orderable: false,
					render: function (data, type, full, meta) {
						return   '\<a onclick = "updateSlayt(\'' + data + '\')" class="btn btn-sm btn-clean btn-icon" title="Edit details">\
								<i class="la la-edit"></i>\
							</a>\
							<a onclick = "slaytSil(\'' + data + '\')" class="btn btn-sm btn-clean btn-icon" title="Delete">\
								<i class="la la-trash"></i>\
							</a>\ ';
					},
				},
				{
					targets: 2,
					render: function (data, type, full, meta) {
						if (data != null && data.length > 0)
							return '<img class="h-100px rounded-sm" src="' + website + 'homeslider/' + data + '" alt="....">';
						else { return ''; }
					},
				},
				{
					targets: 3,
					render: function (data, type, full, meta) {
						if (data != null && data.length > 0)
							return '<img class="h-100px rounded-sm" src="' + website + 'homeslider/' + data + '" alt="....">';
						else { return ''; }
					},
				},
				{
					targets: 9,
					render: function (data, type, full, meta) {
						if (data)
							return '<i class="flaticon2-checkmark text-success"></i>';
						else return '<i class="flaticon2-delete text-danger"></i>';
					},
				},
				{
					targets: 10,
					render: function (data, type, full, meta) {
						return dilne(data);
					},
				},
				
				//{
				//	width: '75px',
				//	targets: -3,
				//	render: function (data, type, full, meta) {
				//		var status = {
				//			1: { 'title': 'Pending', 'class': 'label-light-primary' },
				//			2: { 'title': 'Delivered', 'class': ' label-light-danger' },
				//			3: { 'title': 'Canceled', 'class': ' label-light-primary' },
				//			4: { 'title': 'Success', 'class': ' label-light-success' },
				//			5: { 'title': 'Info', 'class': ' label-light-info' },
				//			6: { 'title': 'Danger', 'class': ' label-light-danger' },
				//			7: { 'title': 'Warning', 'class': ' label-light-warning' },
				//		};
				//		if (typeof status[data] === 'undefined') {
				//			return data;
				//		}
				//		return '<span class="label label-lg font-weight-bold' + status[data].class + ' label-inline">' + status[data].title + '</span>';
				//	},
				//},
				//{
				//	width: '75px',
				//	targets: -2,
				//	render: function (data, type, full, meta) {
				//		var status = {
				//			1: { 'title': 'Online', 'state': 'danger' },
				//			2: { 'title': 'Retail', 'state': 'primary' },
				//			3: { 'title': 'Direct', 'state': 'success' },
				//		};
				//		if (typeof status[data] === 'undefined') {
				//			return data;
				//		}
				//		return '<span class="label label-' + status[data].state + ' label-dot mr-2"></span>' +
				//			'<span class="font-weight-bold text-' + status[data].state + '">' + status[data].title + '</span>';
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




var secilenDeger;
var update = false;
var resim1 = "";
var resim2 = "";
function yeniKayit() {
	$('#exampleModalSizeLg').modal('show');
	$(':input', this).val('');
	$('#listFoto1 > .image-input-wrapper').css('background-image', 'url( )').trigger('change');
	$('#listFoto2 > .image-input-wrapper').css('background-image', 'url( )').trigger('change');
	resim1 = "";
	resim2 = "";
	update = false;
}




function updateSlayt(pid) {


	secilenDeger = $('#kt_datatable').DataTable().data().filter(x => x.Id == pid)[0];

	$('#Id').val(secilenDeger.Id);
	$("#Name").val(secilenDeger.Name);
	$("#Link").val(secilenDeger.Link);
	$("#TargetId").val(secilenDeger.Target).trigger('change');
	$("#RowNum").val(secilenDeger.RowNum);
	$("#Type").val(secilenDeger.Type);

	if (secilenDeger.Status == 1) {
		document.getElementById("status").checked = true;
	} else {
		document.getElementById("status").checked = false;
	}

	$('#listFoto1 > .image-input-wrapper').css('background-image', 'url(' + website + 'homeslider/' + secilenDeger.Image1 + ')').trigger('change');
	$('#listFoto2 > .image-input-wrapper').css('background-image', 'url(' + website + 'homeslider/' + secilenDeger.Image2 + ')').trigger('change');

	resim1 = secilenDeger.Image1;
	resim2 = secilenDeger.Image2;

	update = true;
	//var submitButton = document.getElementById('btnSubmit');
	//submitButton.setAttribute('onclick', 'satisNoktasiGuncelle()');

	$('#exampleModalSizeLg').modal('show');

}

					

function slaytKaydet(pid) {
	$('#exampleModalSizeLg').pleaseWait();
	var slider = {
		'Id': 0,
		"Image1": resim1,
		"Image2": resim2,
		'Name': $("#Name").val(),
		'Link': $("#Link").val(),
		'Target': $("#TargetId").val(),
		'Type': $("#Type").val(),
		'RowNum': $("#RowNum").val(),
		'Status': $('#status').is(':checked') == true ? 1 : 0,
	};
	var dtt;
	if (update) {

		secilenDeger.Name = slider.Name;
		secilenDeger.Description = slider.Link;
		secilenDeger.Target = slider.Target;
		secilenDeger.Image1 = slider.Image1;
		secilenDeger.Image2 = slider.Image2;
		secilenDeger.Type = slider.Type;
		secilenDeger.RowNum = slider.RowNum;
		secilenDeger.Status = slider.Status;
	

		dtt = { slayt: secilenDeger };
	} else {
		dtt = { slayt: slider };
	}
	console.log(dtt);
	$.ajax({
		data: dtt,
		dataType: 'json',
		cache: false,
		type: "POST",
		url: '/' + lngg + '/Slayt/createSlayt',
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


function slaytSil(pid) {

	swal.fire({
		text: "Id= " + pid + "  Slaytı Silmek istiyor musunuz?",
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
				data: { slayt: { Id: pid } },
				dataType: 'json',
				cache: false,
				type: "POST",
				url: '/' + lngg + '/Slayt/deleteSlayt',
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



var avatar1 = new KTImageInput('listFoto1');

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
	resimYukle(1, imageInput, "listFoto1");
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


var avatar2 = new KTImageInput('listFoto2');

avatar2.on('cancel', function (imageInput) {
	resim2 = "";
	swal.fire({
		title: 'Resim İptal',
		type: 'success',
		buttonsStyling: false,
		confirmButtonText: 'OK',
		confirmButtonClass: 'btn btn-primary font-weight-bold'
	});
});

avatar2.on('change', function (imageInput) {
	resimYukle(2, imageInput, "listFoto2");
});

avatar2.on('remove', function (imageInput) {
	resim2 = "";
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
		url: '/' + lngg + '/Slayt/UploderImage',
		type: 'POST',
		success: function (data) {
			if (data != 'false') {
				if (hng == 1) {
					resim1 = data;
				} if (hng == 2) {
					resim2 = data;
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
				resim2 = "";
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