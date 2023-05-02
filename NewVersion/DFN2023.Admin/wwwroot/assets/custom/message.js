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
			dom: `<'row'<'col-sm-6 text-left'f><'col-sm-6 text-right'B>>
			<'row'<'col-sm-12'tr>>
			<'row'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7 dataTables_pager'lp>>`,
			buttons: [
				'print',
				'copyHtml5',
				'excelHtml5',
				'csvHtml5',
				'pdfHtml5',
			],
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
				{ data: 'FromUser' },
				{ data: 'ToUser' },
				{ data: 'FromRolId' },
				{ data: 'MessageContent' },
				//{ data: 'FromUser' },
				//{ data: 'ToUser' },
				//{ data: 'FromRolId' },
				{ data: 'LastIP' },
				{ data: 'Status' },

				//{ data: 'Name' },
				//{ data: 'Email' },
				//{ data: 'Phone' },
				//{ data: 'Message' },
				//{ data: 'KVKK1' },
				//{ data: 'KVKK2' },
				//{ data: 'Date2' },
				/*{ data: 'LangId' },*/
				/*{ data: 'Status' },*/
				
			],
			columnDefs: [
				{
				targets: 0,
				title: 'Actions',
				orderable: false,
				render: function (data, type, full, meta) {
					return '\<a onclick = "updateKayit(\'' + data + '\')" class="btn btn-sm btn-clean btn-icon" title="İşlem yap">\
								<i class="la la-edit"></i>\
							</a>\
							<a onclick = "SilIletisim(\'' + data + '\')" class="btn btn-sm btn-clean btn-icon" title="Delete">\
								<i class="la la-trash"></i>\
							</a>\ ';
				} 
				}, 
				//{
				//	targets: 9,
				//	render: function (data, type, full, meta) {
				//		return dilne(data);
				//	},
				//}, 
				//{
				//	targets: 10,
				//	render: function (data, type, full, meta) {
				//		if (data)
				//			return 'Evet';
				//		else return 'Hayır ';
				//	},
				//},  {
				//	targets: 6,
				//	render: function (data, type, full, meta) {
				//		if (data)
				//			return 'Evet';
				//		else return 'Hayır ';
				//	},
				//},   {
				//	targets: 7,
				//	render: function (data, type, full, meta) {
				//		if (data)
				//			return 'Evet';
				//		else return 'Hayır ';
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

function SilIletisim(Id) {


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
				url: hst1,
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

function updateKayit(Id) {


	swal.fire({
		title: 'İşlem Yapıldı mı?',
		text: "Evet veya Hayır İşaretleyiniz",
		icon: 'question',
		showCancelButton: true,
		confirmButtonColor: '#d33',
		confirmButtonText: 'Evet',
		cancelButtonText: "Hayır",
		cancelButtonColor: '#1ab394',
	}).then(function (result) {
		console.log(result)
		if (result.value) {
			console.log(result)
			$.ajax({
				type: "POST",
				url: hst2,
				dataType: "json",
				data: {
					"Id": Id, "Status": 1
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
			});
		}
		if (result.dismiss == 'cancel') {
			console.log(result)
			$.ajax({
				type: "POST",
				url: hst2,
				dataType: "json",
				data: {
					"Id": Id, "Status": 0
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
			});
		}
	});


}

jQuery(document).ready(function () {
	KTDatatablesDataSourceAjaxServer.init();
});

function yeniKayit() {
	$('#exampleModalSizeLg').modal('show');
}
