
function filter() {
        $.ajax({
            data: {
                kid: $('#kategoriid').val(),
                urun: $('#urunadi').val(),
            },
            type: "POST",
            url: hst,
            success: function () {

                window.location.href = '/Tedarikci/List';
            },

        });
    

}


function favEkleCikar(cid, durum) {
    if (user.length>0) {
        $.ajax({
            data: {
                companyid: cid,
                durum: durum,
            },
            type: "POST",
            url: hst2,
            success: function (data) {
                if (!data.hata) {

                    swal.fire({
                        text: data.mesaj,
                        icon: "success",
                       
                    });

                    window.location.reload();
                }
                else {
                    Swal.fire(
                        'HATA',
                        data.mesaj,
                        'error'
                    )
                }
             //   window.location.href = '/Tedarikci/List';
            },

        });
    } else {
        girisYapModal();
    }

}
var harita = false;
function haritaAc() {
   
    if (harita) {
        harita = false;
        document.getElementById("urunler").style.display = "block";
        document.getElementById("harita").style.display = "none";
    } else {
        harita = true;
        document.getElementById("urunler").style.display = "none";
        document.getElementById("harita").style.display = "block";

    }
  
}
var loc = [];
document.addEventListener("DOMContentLoaded", () => {
    $("#kategoriid").val(skate).trigger('change');
    for (var i = 0; i < maplist.length; i++) {
        loc.push({ lat: parseFloat(maplist[i].MapX), lng: parseFloat(maplist[i].MapY) });
    
    }
});

function initMap() {//center: new google.maps.LatLng(parseFloat(maplist[0].MapX), parseFloat(maplist[0].MapY)),
    const uluru = { lat: 37.872518771338065, lng: 32.492297107014956};
    const uluru2 = { lat: 37.87671933198883, lng: 32.47856419752655 };
    const uluru3 = { lat: 37.85530752238008, lng: 32.54963200412905 };
    const uluru4 = { lat: 37.890674313416746, lng: 32.53658574011506 };
    const uluru5 = { lat: 37.89053884087271, lng: 32.48594563637657 };
    const map = new google.maps.Map(document.getElementById("map"), {
        zoom: 10,
        center: uluru,
    });
    const contentString =
        '<div id="content">' +
        '<div id="siteNotice">' +
        "</div>" +
        '<h1 id="firstHeading" class="firstHeading">Uluru</h1>' +
        '<div id="bodyContent">' +
        "<p><b>Uluru</b>, also referred to as <b>Ayers Rock</b>, is a large " +
        "sandstone rock formation in the southern part of the " +
        "Northern Territory, central Australia. It lies 335&#160;km (208&#160;mi) " +
        "Heritage Site.</p>" +
        '<p>Attribution: Uluru, <a href="https://en.wikipedia.org/w/index.php?title=Uluru&oldid=297882194">' +
        "https://en.wikipedia.org/w/index.php?title=Uluru</a> " +
        "(last visited June 22, 2009).</p>" +
        "</div>" +
        "</div>";
    //for (var i = 0; i < loc.length; i++) {
    //    const marker =  new google.maps.Marker({
    //        position: loc[i],
    //        map,
    //        title: "Uluru (Ayers Rock)",
    //    });
    //}
    const marker = new google.maps.Marker({
        position: uluru,
        map,
        title: "Uluru (Ayers Rock)",
    });

    const marker2 = new google.maps.Marker({
        position: uluru2,
        map,
        title: "Uluru (Ayers Rock)",
    });
    const marker3 = new google.maps.Marker({
        position: uluru3,
        map,
        title: "Uluru (Ayers Rock)",
    });
    const marker4 = new google.maps.Marker({
        position: uluru4,
        map,
        title: "Uluru (Ayers Rock)",
    });
    const marker5 = new google.maps.Marker({
        position: uluru5,
        map,
        title: "Uluru (Ayers Rock)",
    });
    marker.addListener("click", () => {
        new google.maps.InfoWindow({
            content: contentString,
            ariaLabel: "Uluru",
        }).open({
            anchor: new google.maps.Marker({
                position: uluru,
                map,
                title: "Uluru (Ayers Rock)",
            }),
            map,
        });
    });
    marker2.addListener("click", () => {
        new google.maps.InfoWindow({
            content: contentString,
            ariaLabel: "Uluru",
        }).open({
            anchor: new google.maps.Marker({
                position: uluru2,
                map,
                title: "Uluru (Ayers Rock)",
            }),
            map,
        });
    });
    marker3.addListener("click", () => {
        new google.maps.InfoWindow({
            content: contentString,
            ariaLabel: "Uluru",
        }).open({
            anchor: new google.maps.Marker({
                position: uluru3,
                map,
                title: "Uluru (Ayers Rock)",
            }),
            map,
        });
    });
    marker4.addListener("click", () => {
        new google.maps.InfoWindow({
            content: contentString,
            ariaLabel: "Uluru",
        }).open({
            anchor: new google.maps.Marker({
                position: uluru4,
                map,
                title: "Uluru (Ayers Rock)",
            }),
            map,
        });
    });
    marker5.addListener("click", () => {
        new google.maps.InfoWindow({
            content: contentString,
            ariaLabel: "Uluru",
        }).open({
            anchor: new google.maps.Marker({
                position: uluru5,
                map,
                title: "Uluru (Ayers Rock)",
            }),
            map,
        });
    });
    //marker.addListener("click", () => {
        for (var i = 0; i < locations.length; i++) {
            new google.maps.InfoWindow({
                content: contentString,
                ariaLabel: "Uluru",
            }).open({
                anchor: new google.maps.Marker({
                    position: locations[i],
                    map,
                    title: "Uluru (Ayers Rock)",
                }),
                map,
            });
        }
     
        
    //});

  ////for (var i = 0; i < locations.length; i++) {
  //      marker.addListener("click", () => {
  //          new google.maps.InfoWindow({
  //              content: contentString,
  //              ariaLabel: "Uluru",
  //          }).open({
  //              anchor: new google.maps.Marker({
  //                  position: locations[i],
  //                  map,
  //                  title: "Uluru (Ayers Rock)",
  //              }),
  //              map,
  //          });
  //      });
  //  marker2.addListener("click", () => {
  //      new google.maps.InfoWindow({
  //          content: contentString,
  //          ariaLabel: "Uluru",
  //      }).open({
  //          anchor: new google.maps.Marker({
  //              position: locations[i],
  //              map,
  //              title: "Uluru (Ayers Rock)",
  //          }),
  //          map,
  //      });
  //  });
  //  marker4.addListener("click", () => {
  //      new google.maps.InfoWindow({
  //          content: contentString,
  //          ariaLabel: "Uluru",
  //      }).open({
  //          anchor: new google.maps.Marker({
  //              position: locations[i],
  //              map,
  //              title: "Uluru (Ayers Rock)",
  //          }),
  //          map,
  //      });
  //  });
  //  marker5.addListener("click", () => {
  //      new google.maps.InfoWindow({
  //          content: contentString,
  //          ariaLabel: "Uluru",
  //      }).open({
  //          anchor: new google.maps.Marker({
  //              position: locations[i],
  //              map,
  //              title: "Uluru (Ayers Rock)",
  //          }),
  //          map,
  //      });
  //  });
  ////}

  //  for (var i = 0; i < locations.length; i++) {
        

  //      new google.maps.Marker({
  //          position: locations[i],
  //          map,
  //          title: "Uluru (Ayers Rock)",
  //      }).addListener("click", () => {
  //      new google.maps.InfoWindow({
  //          content: contentString,
  //          ariaLabel: "Uluru",
  //      }).open({
  //          anchor: new google.maps.Marker({
  //              position: locations[i],
  //              map,
  //              title: "Uluru (Ayers Rock)",
  //          }),
  //          map,
  //      });
  //  });
   
   
  //}
}

window.initMap = initMap;

//var locations = [

//];

//var map = new google.maps.Map(document.getElementById('map'), {
//    zoom: 9,
//    /* Zoom level of your map */
//    center: new google.maps.LatLng(parseFloat(maplist[0].MapX), parseFloat(maplist[0].MapY)),
//    /* coordinates for the center of your map */
//    mapTypeId: google.maps.MapTypeId.ROADMAP
//});

//var infowindow = new google.maps.InfoWindow();

//var marker, i;

//for (i = 0; i < locations.length; i++) {
//    marker = new google.maps.Marker({
//        position: new google.maps.LatLng(locations[i][1], locations[i][2]),
//        map: map
//    });

//    google.maps.event.addListener(marker, 'click', (function (marker, i) {
//        return function () {
//            infowindow.setContent(locations[i][0]);
//            infowindow.open(map, marker);
//        }
//    })(marker, i));
//}