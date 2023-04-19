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
				{ data: 'UserName' },
				{ data: 'Password' },
				{ data: 'Name' },
				{ data: 'Surname' },
				{ data: 'Email' },
				{ data: 'Phone' },
				{ data: 'Role' },
				{ data: 'Status' },
			],
			columnDefs: [
				{
					targets: 0,
					title: 'Actions',
					orderable: false,
					render: function (data, type, full, meta) {
						return  '\<a onclick = "userDuzenle(\'' + data + '\')" class="btn btn-sm btn-clean btn-icon" title="Edit details">\
								<i class="la la-edit"></i>\
							</a>\
							<a onclick = "userSil(\'' + data + '\')" class="btn btn-sm btn-clean btn-icon" title="Delete">\
								<i class="la la-trash"></i>\
							</a>\ ';
					},
				},

				{
					targets: 9,
					render: function (data, type, full, meta) {
						if (data)
							return '<i class="flaticon2-checkmark text-success"></i>';
						return '<i class="flaticon2-delete text-danger"></i>';
					},
				}, {
					targets: 8,
					render: function (data, type, full, meta) {
						if (data)
							return 'Admin';
						return 'User';
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
	update = false;
}

var secilendeger;
function userDuzenle(pid) {
	update = true;
	$('#exampleModalSizeLg').modal('show');
	secilendeger = $('#kt_datatable').DataTable().data().filter(x => x.Id == pid)[0];

	$('#p_UserName').val(secilendeger.UserName); 
	$('#p_Name').val(secilendeger.Name);
	$('#p_Surname').val(secilendeger.Surname);
	$('#p_Email').val(secilendeger.Email);
	$('#p_Phone').val(secilendeger.Phone);
	$('#p_Role').val(secilendeger.Role).trigger('change');
	$("#p_durum").prop('checked', secilendeger.Status == 1 ? true : false);

}



function seriKaydet() {

	var product = {
		'UserName': $('#p_UserName').val(),
		'Password': $('#p_Password').val(),
		'Password2': $('#p_Passworda').val(),
		'Name': $('#p_Name').val(),
		'Surname': $('#p_Surname').val(),
		'Email': $('#p_Email').val(),
		'Phone': $('#p_Phone').val(),
		'Role': $('#p_Role').val(),
		'Status': $('#p_durum').is(':checked') == true ? 1 : 0,


	};
	if (product.Password == product.Password2) {
		var dtt;
		if (update) {

			secilendeger.UserName = product.UserName;
			secilendeger.Password = product.Password;
			secilendeger.Name = product.Name;
			secilendeger.Surname = product.Surname;
			secilendeger.Email = product.Email;
			secilendeger.Phone = product.Phone;
			secilendeger.Role = product.Role;
			secilendeger.Status = product.Status;


			dtt = { usr: secilendeger };
		} else {
			dtt = { usr: product };
		}


	$('#exampleModalSizeLg').pleaseWait();
		$.ajax({
			data: dtt,
			dataType: 'json',
			cache: false,
			type: "POST",
			url: '/' + lngg + '/User/CreatedUser',
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
	else {
		alert("Şifreniz uyumlu değil");
    }
}



function userSil(pid) {

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
				data: { usr: { Id: pid } },
				dataType: 'json',
				cache: false,
				type: "POST",
				url: '/' + lngg + '/User/DeleteUser',
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


//columns: [
//	{ data: 'Id' },
//	{ data: 'Id' },
//	{ data: 'UserName' },
//	{ data: 'Password' },
//	{ data: 'Name' },
//	{ data: 'Surname' },
//	{ data: 'Email' },
//	{ data: 'Phone' },
//	{ data: 'Role' },
//	{ data: 'Status' },
//],
