
document.addEventListener("DOMContentLoaded", () => {



    $("#haritaulke").change(function () {
        var id = $(this).val();
        $("#haritail").empty();
        $.ajax({
            data: { CountryId: id },
            dataType: 'json',
            cache: false,
            async: false,
            type: "POST",
            url: hst3,
            success: function (data) {
                hgelencity = data;
                var v = " <option  value='0'> Şehir Seçiniz </option>";
                $.each(data, function (i, v1) {
                    v += "<option value=" + v1.Value + ">" + v1.Text + "</option>";
                });
                $("#haritail").html(v);
            },
        });
    });

    $("#haritail").change(function () {
        var id = $(this).val();
        $("#haritailce").empty();
        $.ajax({
            data: { CityId: id },
            dataType: 'json',
            cache: false,
            async: false,
            type: "POST",
            url: hst4,
            success: function (data) {
                hgelendist = data;
                var v = " <option  value='0'> İlçe Seçiniz </option>";
                $.each(data, function (i, v1) {
                    v += "<option value=" + v1.Value + ">" + v1.Text + "</option>";
                });
                $("#haritailce").html(v);
            },
        });
    });
    $("#ulke").change(function () {
        var id = $(this).val();
        $("#il").html("");
        $.ajax({
            data: { CountryId: id },
            dataType: 'json',
            cache: false,
            async: false,
            type: "POST",
            url: hst3,
            success: function (data) {
                gelencity = data;
                var v = " <option  value='0'> Şehir Seçiniz </option>";
                $.each(data, function (i, v1) {
                    v += "<option value=" + v1.Value + ">" + v1.Text + "</option>";
                });
                $("#il").html(v);
            },
        });
    });

    $("#il").change(function () {
        var id = $(this).val();
        $("#ilce").empty();
        $.ajax({
            data: { CityId: id },
            dataType: 'json',
            cache: false,
            async: false,
            type: "POST",
            url: hst4,
            success: function (data) {
                gelendist = data;
                var v = " <option  value='0'> İlçe Seçiniz </option>";
                $.each(data, function (i, v1) {
                    v += "<option value=" + v1.Value + ">" + v1.Text + "</option>";
                });
                $("#ilce").html(v);
            },
        });
    });
});


var gelendist = [];

var gelencity = [];

var hgelendist = [];

var hgelencity = [];

function firmaKaydet(cntl) {
    var pid = 0;
    if (cntl==2 && sirket!=null) {
        pid = sirket.Id;
    } 
    var mr = $("#marka").val();
    var srun = $("#sirkerunvani").val();
    var vd = $("#vergidairesi").val();
    var vkn = $("#vkn").val();

    var ul = $("#ulke").val();
    var ktyp = $("#kurumtip").val();
    var il = $("#il").val();
    var ilc = $("#ilce").val();
    var adr = $("#adres").val();
    var email = $("#emailadres").val();
    var tel = $("#tel").val();
    var ceptel = $("#ceptel").val();
    var web = $("#web").val();

    var inst = $("#instagram").val();
    var yt = $("#youtube").val();
    var hil = $("#haritail").val();
    var hilc = $("#haritailce").val();
    var hul = $("#haritaulke").val();
    var hadr = $("#hadres").val();
    var kx = $("#kx").val().trim();
    var ky = $("#ky").val().trim();
    var ddesc = $("#ddesc").val();
    var sdesc = $("#sdesc").val();

    if (mr.length < 1) {

        uyari("Marka Adı boş geçilemez");
    }
    else if (vkn.length < 1) {

        uyari("VKN boş geçilemez");
    }
    else if (srun.length < 1) {

        uyari("Şirket ünvanı boş geçilemez");
    }
    else if (ktyp <= 0) {

        uyari("Şirket tipi seçiniz");
    }
    else if (adr.length < 1 && hadr.length < 1) {

        uyari("Adres Alanları boş geçilemez");
    }
    else if (ceptel.length < 1 && tel.length < 1  ) {

        uyari("Telefon Alanları boş geçilemez");
    }
    else if (email.length < 1) {

        uyari("Email Alanı boş geçilemez");
    }
    else if (kx.length < 1 && ky.length < 1) {

        uyari("Koordinat alanları boş geçilemez");
    }
    else {
        $.ajax({
            type: "POST",
            async: true,
            url: hst2,
            data: {
                Id=pid,
                BrandName:mr,
                OfficialName: srun ,
                ShortDescription: sdesc ,
                DetailDescription: ddesc  ,
                TaxOffice: vd,
                TaxNo: vkn,
                CompanyTypeId: ktyp,
                OfficialAddress : adr,
         
                OfficialCountryId: ul ,
                OfficialCityId: il ,
                OfficialCountyId: ilc,
                MapAddress: hadr ,
         
                MapCountryId: hul ,
                MapCityId: hil ,
                MapCountyId : hilc,

                MapX: kx ,
                MapY: ky ,
                Email: email ,
                Phone: tel ,
                Mobile: ceptel,
                Logo: resim1,
                Attachment: dosya1,
                Instagram: inst ,
                Youtube: yt ,
                Website: web ,
                Status : 1,
                UserId: user.Id,

            },
            dataType: 'json',
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



                        window.location.href = '/Home/Index';



                    });
                } else {
                    uyari(data.mesaj);
                }


            },
            complete: function (data2) {


            },

        });
    }


}
//"p_Image"
var resim1 = "";

function uploadFiles(inputId) {
    var input = document.getElementById(inputId);
    var files = input.files;
    var formData = new FormData();

    for (var i = 0; i != files.length; i++) {
        formData.append("files", files[i]);
    }

    $.ajax({
        data: formData,
        processData: false,
        contentType: false,
        url: hst,
        type: 'POST',
        success: function (data) {
            if (data != 'false') {
                    resim1 = data;
                
                swal.fire({
                    title: 'Resim Yükleme Başarılı',
                    type: 'success',
                    buttonsStyling: false,
                    confirmButtonText: 'OK',
                    confirmButtonClass: 'btn btn-primary font-weight-bold'
                });
            } else {
                resim1 = "";
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



var dosya1 = "";
function uploadFilesPdf(inputId) {
    var input = document.getElementById(inputId);
    var files = input.files;
    var formData = new FormData();

    for (var i = 0; i !== files.length; i++) {
        formData.append("file", files[i]);
    }

    $.ajax(
        {
            url: hst1,
            data: formData,
            processData: false,
            contentType: false,
            type: "POST",
            success: function (data) {

                dosya1 = data;

                    swal.fire({
                        title: 'Dosya Yükleme Başarılı',
                        type: 'success',
                        buttonsStyling: false,
                        confirmButtonText: 'OK',
                        confirmButtonClass: 'btn btn-primary font-weight-bold'
                    });
               
            }
        }
    );
}
//function uploadFilesPdf(inputId) {
  
//    $.ajax({
//        paramName: "file",
//        maxFiles: 1,
//        url: hst1,
//        maxFilesize: 10, // MB
//        addRemoveLinks: true,
//        acceptedFiles: "application/pdf",
//        accept: function (file, done) {

//            dosya1 = file.name;
//            done();
//        },
       
//    });
//}