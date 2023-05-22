var update = false;
var sid = 0;

function guncelleModal(id,c,n,p,u,d) {
	update = true;
	sid = id;
	uguncmodal.style.display = "block";
	$('#gkategoriid').val(c).trigger('change');
    $('#gurun').val(n),
    $('#gfiyat').val(p),
	$('#gbirimid').val(u).trigger('change');
	$('#gaciklama').val(d);

}
function urunGuncelle() {
	if ($('#gkategoriid').val() <= 0) {
		uyari("Kategori seçiniz")
	} else if ($('#gurun').val().length < 1) {
		uyari("Urun adi giriniz")
	} else {
		var product = {
			'Id': sid,
			'CategoryId': $('#gkategoriid').val(),
			'Name': $('#gurun').val(),
			'Price': $('#gfiyat').val(),
			'UnitId': $('#gbirimid').val(),
			'Desc': $('#gaciklama').val(),
		};



		$.ajax({
			data: product,
			dataType: 'json',
			cache: false,
			type: "POST",
			url: hst2,
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
						window.location.reload();
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
}

function urunKaydet() {
	if ($('#kategoriid').val() <= 0) {
		uyari("Kategori seciniz")
	} else if ($('#urun').val().length < 1) {
		uyari("Urun adi giriniz")
	} else {
		var product = {
			'CategoryId': $('#kategoriid').val(),
			'Name': $('#urun').val(),
			'Price': $('#fiyat').val(),
			'UnitId': $('#birimid').val(),
			'Desc': $('#aciklama').val(),
		};



		$.ajax({
			data: product,
			dataType: 'json',
			cache: false,
			type: "POST",
			url: hst2,
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
						window.location.reload();
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
	
	
}



function urunSil(pid) {

	swal.fire({
		text: "Id= " + pid + "  Urunu silmek istiyor musunuz?",
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

			$.ajax({
				data: { comp: { Id: pid } },
				dataType: 'json',
				cache: false,
				type: "POST",
				url: hst3,
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
							window.location.reload();
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

