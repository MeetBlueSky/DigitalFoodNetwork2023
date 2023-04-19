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
					let additionalValues = [];
					additionalValues[0] = $('#search_SCGT').val();
					additionalValues[1] = $('#search_SCT').val();
					additionalValues[2] = $('#search_Text').val();
					additionalValues[5] = $('#search_Status').val();

					additionalValues[6] = $('#search_YearInputMin').val();
					additionalValues[7] = $('#search_YearInputMax').val();

					additionalValues[8] = $('#search_Image').val();
					additionalValues[9] = $('#search_Attachment').val();
					additionalValues[10] = $('#search_Video').val();

					additionalValues[11] = statusChanger;

					d.AdditionalValues = additionalValues;
					return JSON.stringify(d);
				}
			},
			columns: [
				{ data: 'Id' },
				{ data: 'Id' },
				{ data: 'Grup' },
				{ data: 'Temp' },
				{ data: 'Title' },
				//{ data: 'Summary' },
				//{ data: 'Html' },
				{ data: 'OrderNo' },
				{ data: 'Image1' },
				//{ data: 'Image2' },
				//{ data: 'Image3' },      //10
				//{ data: 'Image4' },
				//{ data: 'Image5' },
				{ data: 'Attachment1' },
				//{ data: 'Attachment2' },
				//{ data: 'Attachment3' },     //15
				//{ data: 'Video' },
				//{ data: 'SeoKeywords' },
				//{ data: 'SeoDesc' },
				//{ data: 'Link' },
				//{ data: 'LangId' },
				{ data: 'Statu' },
				{ data: 'Date' },
				{ data: 'GrupId' },
				{ data: 'TempId' },
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
							'		<li class="nav-item"><a class="nav-link" href="#" onclick="staticContentPageDuzenle(' + data + ')"><i class="nav-icon la la-edit"></i><span class="nav-text">Düzenle</span></a></li> ' +
							'		<li class="nav-item"><a class="nav-link" href="#" onclick="staticContentPageSil(' + data + ')"><i class="nav-icon la la-trash"></i><span class="nav-text">Sil</span></a></li> ' +
							'	</ul> ' +
							'</div> ' +
							'</div> ';
					},

				},
				//{
				//	targets: 6,
				//	render: function (data, type, full, meta) {
				//		if (data != null && data.length > 200)
				//			return data.substring(0, 200);
				//		else { return data; }
				//	},
				//},
				{
					targets: 6,
					render: function (data, type, full, meta) {
						if (data != null && data.length > 0)
							return '<img class="h-100px rounded-sm" src="' + website + 'assets/static/' + data + '" alt="....">';
						else { return ''; }
					},
				},
				//{
				//	targets: 9,
				//	render: function (data, type, full, meta) {
				//		if (data != null && data.length > 0)
				//			return '<img class="h-100px rounded-sm" src="' + website + 'static/' + data + '" alt="....">';
				//		else { return ''; }
				//	},
				//},
				//{
				//	targets: 10,
				//	render: function (data, type, full, meta) {
				//		if (data != null && data.length > 0)
				//			return '<img class="h-100px rounded-sm" src="' + website + 'static/' + data + '" alt="....">';
				//		else { return ''; }
				//	},
				//},
				//{
				//	targets: 11,
				//	render: function (data, type, full, meta) {
				//		if (data != null && data.length > 0)
				//			return '<img class="h-100px rounded-sm" src="' + website + 'static/' + data + '" alt="....">';
				//		else { return ''; }
				//	},
				//},
				//{
				//	targets: 12,
				//	render: function (data, type, full, meta) {
				//		if (data != null && data.length > 0)
				//			return '<img class="h-100px rounded-sm" src="' + website + 'static/' + data + '" alt="....">';
				//		else { return ''; }
				//	},
				//},
				{
					targets: 8,
					render: function (data, type, full, meta) {

						if (data == 1)
							return '<i class="flaticon2-checkmark text-success"></i>';
						else if (data == 0) return '<i class="flaticon2-delete text-danger"></i>';
						else return '<i class="icon-xl far fa-file-excel"></i>';
					},
				},
				//{
				//	targets: 19,
				//	orderable: false,
				//	render: function (data, type, full, meta) { 
				//		return '<a class="nav-link" href="' + website + data + full.Id + '" target="_blank" ><span class="nav-text"> ' + data + full.Id +'</span></a>';

				//	},
				//},
				//{
				//	targets: 20,
				//	render: function (data, type, full, meta) {
				//		return dilne(data);
				//	},
				//},
				{
					targets: 9,
					render: function (data, type, full, meta) {
						const date = new Date(data).toLocaleDateString();
						return date
					},
				},
				{
					"targets": [10],
					"visible": false
				},
				{
					"targets": [11],
					"visible": false
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

function dataTableRefresh() {
	$('#kt_datatable').DataTable().ajax.reload();
}

jQuery(document).ready(function () {

	KTDatatablesDataSourceAjaxServer.init();
	//KTCkeditorDocument.init();

	hazirladosya1();
	hazirladosya2();
	hazirladosya3();

	hazirlaDosyaTextEditorImage();

	if (GrupPageId > 0) {
		kayitModalOpen();

		$("#p_GrupId").val(GrupPageId)
	}
});

//var p_url = '';
//class MyUploadAdapter {
//	constructor(loader) {
//		// The file loader instance to use during the upload.
//		this.loader = loader;
//		this.url = hst2;
//	}

//	upload() {
//		return this.loader.file.then(file => new Promise((resolve, reject) => {
//			this._initRequest();
//			this._initListeners(resolve, reject, file);
//			this._sendRequest(file);

//		}));
//	}
	 
	 
//	abort() {
//		if (this.xhr) {
//			this.xhr.abort();
//		}
//	}

//	_initRequest() {
//		const xhr = this.xhr = new XMLHttpRequest();

//		xhr.open('POST', this.url, true);
//		xhr.responseType = 'json';
//	}
//	// Initializes XMLHttpRequest listeners.
//	_initListeners(resolve, reject, file) {
//		const xhr = this.xhr;
//		const loader = this.loader;
//		const genericErrorText = `Couldn't upload file: ${file.name}.`;

//		xhr.addEventListener('error', () => reject(genericErrorText));
//		xhr.addEventListener('abort', () => reject());
//		xhr.addEventListener('load', () => {

//			const response = xhr.response;

//			if (!response || response.error) {
//				return reject(response && response.error ? response.error.message : genericErrorText);
//			}

//			console.log('xhr.response', xhr.response);

//			resolve({
//				default: xhr.response[0]
//			});
//		});

//		if (xhr.upload) {
//			xhr.upload.addEventListener('progress', evt => {
//				if (evt.lengthComputable) {
//					loader.uploadTotal = evt.total;
//					loader.uploaded = evt.loaded;
//				}
//			});
//		}
//	}
//	_sendRequest(file) {
//		const data = new FormData();
//		data.append('upload', file);

//		this.xhr.send(data);
//	}
//}
//function MyCustomUploadAdapterPlugin(editor) {
//	editor.plugins.get('FileRepository').createUploadAdapter = (loader) => {
//		return new MyUploadAdapter(loader);
//	};
//}

//var myeditor;
//var KTCkeditorDocument = function () {
//	// Private functions

//	var demos = function () {

//		DecoupledEditor
//			.create(document.querySelector('#p_Html')
//				, {
//					extraPlugins: [MyCustomUploadAdapterPlugin],
//				})
//			.then(p_Ack => {
//				const toolbarContainer = document.querySelector('#kt-ckeditor-3-toolbar');
//				toolbarContainer.appendChild(p_Ack.ui.view.toolbar.element);
//				myeditor = p_Ack;

//			}); 
//	}
//	return {
//		// public functions
//		init: function () {
//			demos();
//		}
//	};
//}();

var dz1;
var dosya1 = "";
function hazirladosya1() {
	dz1 = $('#kt_dropzone_4').dropzone({
		url: hst3,
		paramName: "file",
		maxFiles: 10,
		maxFilesize: DocSizeMax, // MB
		addRemoveLinks: true,
		acceptedFiles: ".pdf,.pptx,.xlsx,.docx,.ppt,.xls,.doc",
		accept: function (file, done) {

			dosya1 = file.name;
			done();
		}
	});
	$('#kt_dropzone_4').on('click', function () {
		$(".dropzone-msg1").hide();
	});

}

var dz2;
var dosya2 = "";
function hazirladosya2() {
	dz2 = $('#kt_dropzone_5').dropzone({
		url: hst3,
		paramName: "file",
		maxFiles: 10,
		maxFilesize: DocSizeMax, // MB
		addRemoveLinks: true,
		acceptedFiles: ".pdf,.pptx,.xlsx,.docx,.ppt,.xls,.doc",
		accept: function (file, done) {

			dosya2 = file.name;
			done();
		}
	});
	$('#kt_dropzone_5').on('click', function () {
		$(".dropzone-msg2").hide();
	});

}

var dz3;
var dosya3 = "";
function hazirladosya3() {
	dz3 = $('#kt_dropzone_6').dropzone({
		url: hst3,
		paramName: "file",
		maxFiles: 10,
		maxFilesize: DocSizeMax, // MB
		addRemoveLinks: true,
		acceptedFiles: ".pdf,.pptx,.xlsx,.docx,.ppt,.xls,.doc",
		accept: function (file, done) {

			dosya3 = file.name;
			done();
		}
	});
	$('#kt_dropzone_6').on('click', function () {
		$(".dropzone-msg3").hide();
	});

}

var textEditorDropzone;
var dz4;
var dosya4 = "";
function hazirlaDosyaTextEditorImage() {
	dz4 = $('#kt_dropzone_7').dropzone({
		url: hst3,                             //?
		paramName: "file",
		maxFiles: 1,
		maxFilesize: DocSizeMax, // MB
		addRemoveLinks: true,
		acceptedFiles: ".jpg,.jpeg,.png",
		accept: function (file, done) {

			dosya4 = file.name;
			done();
		},
		init: function () {

		    textEditorDropzone = this;
		}
	});
	$('#kt_dropzone_7').on('click', function () {
		$(".dropzone-msg4").hide();
	});

}


var richTextEditorjscfg = {}
//richTextEditorjscfg.svgCode_pic = '<svg viewBox="-2 -2 20 20" fill="#5F6368" style="width: 100%; height: 100%;"><path fill-rule="evenodd" d="M8 0a1 1 0 0 1 1 1v5.268l4.562-2.634a1 1 0 1 1 1 1.732L10 8l4.562 2.634a1 1 0 1 1-1 1.732L9 9.732V15a1 1 0 1 1-2 0V9.732l-4.562 2.634a1 1 0 1 1-1-1.732L6 8 1.438 5.366a1 1 0 0 1 1-1.732L7 6.268V1a1 1 0 0 1 1-1z" clip-rule="evenodd"></path></svg>';

richTextEditorjscfg.svgCode_insertpicture = '<svg fill="#000000" height="100%" width="100%" version="1.1" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 350 350" xmlns:xlink="http://www.w3.org/1999/xlink" enable-background="new 0 0 350 350"><path d="M5,350h340V0H5V350z M25,330v-62.212h300V330H25z M179.509,247.494H60.491L120,171.253L179.509,247.494z   M176.443,211.061l33.683-32.323l74.654,69.05h-79.67L176.443,211.061z M325,96.574c-6.384,2.269-13.085,3.426-20,3.426  c-33.084,0-60-26.916-60-60c0-6.911,1.156-13.612,3.422-20H325V96.574z M25,20h202.516C225.845,26.479,225,33.166,225,40  c0,44.112,35.888,80,80,80c6.837,0,13.523-0.846,20-2.518v130.306h-10.767l-104.359-96.526l-45.801,43.951L120,138.748  l-85.109,109.04H25V20z" /><g><text x="0" y="15">Label 1</text></g ></svg>';
richTextEditorjscfg.svgCode_insertpicture2 = '<svg fill="#000000" height="100%" width="100%" version="1.1" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 350 350" xmlns:xlink="http://www.w3.org/1999/xlink" enable-background="new 0 0 350 350"><path d="M5,350h340V0H5V350z M25,330v-62.212h300V330H25z M179.509,247.494H60.491L120,171.253L179.509,247.494z   M176.443,211.061l33.683-32.323l74.654,69.05h-79.67L176.443,211.061z M325,96.574c-6.384,2.269-13.085,3.426-20,3.426  c-33.084,0-60-26.916-60-60c0-6.911,1.156-13.612,3.422-20H325V96.574z M25,20h202.516C225.845,26.479,225,33.166,225,40  c0,44.112,35.888,80,80,80c6.837,0,13.523-0.846,20-2.518v130.306h-10.767l-104.359-96.526l-45.801,43.951L120,138.748  l-85.109,109.04H25V20z" /><g><text x="0" y="15">Label 1</text></g ></svg>';


richTextEditorjscfg.toolbar = "mytoolbar";

richTextEditorjscfg.toolbar_mytoolbar =
	"{bold,italic,underline,forecolor,backcolor}|{justifyleft,justifycenter,justifyright,justifyfull}|{insertorderedlist,insertunorderedlist,indent,outdent,insertblockquote,insertemoji}"
	+ "#{paragraphs:toggle,fontname:toggle,fontsize:toggle,inlinestyle:toggle,lineheight:toggle}"
+ "/{removeformat,cut,copy,paste,delete,find}|{insertlink,insertchars,inserttable,insertpicture,insertpicture2,insertvideo,insertdocument,inserttemplate,insertcode}|{preview,code,selectall}"
	+ "#{toggleborder,fullscreenenter,fullscreenexit,undo,redo,togglemore}";

var richTextEditorjsHTML = new RichTextEditor("#p_Html", richTextEditorjscfg);
var richTextEditorjsSummary = new RichTextEditor("#p_Summary", richTextEditorjscfg);

var editorSelect = '';
richTextEditorjsHTML.attachEvent("exec_command_insertpicture", function (state, cmd, value) {
	state.returnValue = true;//set it has been handled
	editorSelect = 'HTML';

	textEditorDropzone.removeAllFiles(true);
	$('.dropzone-msg-title4').html('Resimleri Seçiniz');
	$('.dropzone-msg-desc4').html('Bu alana tıklayarak yeni resim seçebilirsiniz.');

	$('#textEditorImageModal').modal('show');
});
richTextEditorjsSummary.attachEvent("exec_command_insertpicture", function (state, cmd, value) {
	state.returnValue = true;//set it has been handled
	editorSelect = 'Summary';

	textEditorDropzone.removeAllFiles(true);
	$('.dropzone-msg-title4').html('Resimleri Seçiniz');
	$('.dropzone-msg-desc4').html('Bu alana tıklayarak yeni resim seçebilirsiniz.');

	$('#textEditorImageModal').modal('show');
});
richTextEditorjsHTML.attachEvent("exec_command_insertpicture2", function (state, cmd, value) {
	state.returnValue = true;//set it has been handled
	/*$('#rowImageModal').modal('show');*/
	showAllImages();

});
richTextEditorjsSummary.attachEvent("exec_command_insertpicture2", function (state, cmd, value) {
	state.returnValue = true;//set it has been handled
	/*$('#rowImageModal').modal('show');*/
	showAllImages();

});

function textEditorResimKaydet(SelectedEditor) {

	//editor.setContents('');
	//editor.insertHTML($('#sourceKode').val(), true, true, false);

	/*myeditor.setData($('#sourceKode').val());*/
	/*$('#sourcekodeModel').modal('hide');*/

	$('#textEditorImageModal').modal('hide');
	$('#exampleModalSizeLg').css("overflow-y", "auto");

	var imageHTML = '<img style="max-width:80%;" src="~/' + rootFolder + '/' + sunucuEntityDosyasi + '/' + dosya4 + '"/>';

	if (SelectedEditor == 'HTML') {
		richTextEditorjsHTML.insertHTML(imageHTML);
		richTextEditorjsHTML.collapse(false);
		richTextEditorjsHTML.focus();
	}
	else if ((SelectedEditor == 'Summary')) {
		richTextEditorjsSummary.insertHTML(imageHTML);
		richTextEditorjsSummary.collapse(false);
		richTextEditorjsSummary.focus();
	}
}


//function kaynakgoster() {
//	$('#sourcekodeModel').modal('show');
//	/*$('#sourceKode').val(myeditor.getData());*/
//	/*$('#sourceKode').val(editor.getContents(true));*/
//	$('#sourceKode').val(richTextEditorjs.getHTMLCode());
//}
//function sourceKodeKaydet() {

//	$('#sourceKode').val(richTextEditorjs.getHTMLCode());
//	/*myeditor.setData($('#sourceKode').val());*/
//	$('#sourcekodeModel').modal('hide');
//	$('#exampleModalSizeLg').css("overflow-y", "auto");
//}

var update = false;
var resim1 = "";
var resim2 = "";
var resim3 = "";
var resim4 = "";
var resim5 = "";

function kayitModalOpen() {

	$('#exampleModalSizeLg').modal('show');
	$(':input').val('');
	update = false;
	$('#p_Foto1 > .image-input-wrapper').css('background-image', 'url( )').trigger('change');
	$('#p_Foto2 > .image-input-wrapper').css('background-image', 'url( )').trigger('change');
	$('#p_Foto3 > .image-input-wrapper').css('background-image', 'url( )').trigger('change');
	$('#p_Foto4 > .image-input-wrapper').css('background-image', 'url( )').trigger('change');
	$('#p_Foto5 > .image-input-wrapper').css('background-image', 'url( )').trigger('change');
	resim1 = "";
	resim2 = "";
	resim3 = "";
	resim4 = "";
	resim5 = "";

	$(".dropzone-msg-title1").html("Döküman Seç");
	$(".dropzone-msg-desc1").html("Bu alana tıklayarak yeni döküman seçebilirsiniz.");
	$(".dropzone-msg-title2").html("Döküman Seç");
	$(".dropzone-msg-desc2").html("Bu alana tıklayarak yeni döküman seçebilirsiniz.");
	$(".dropzone-msg-title3").html("Döküman Seç");
	$(".dropzone-msg-desc3").html("Bu alana tıklayarak yeni döküman seçebilirsiniz.");

	dosya1 = "";
	dosya2 = "";
	dosya3 = "";

	richTextEditorjsHTML.clearHistory();
	richTextEditorjsHTML.setHTMLCode('');
	if (richTextEditorjsHTML.isCommandActive("code")) {                                      // code source açık ise bu işlem yapılacak
		richTextEditorjsHTML.execCommand("code", "false");
	}
	richTextEditorjsSummary.clearHistory();
	richTextEditorjsSummary.setHTMLCode('');
	if (richTextEditorjsSummary.isCommandActive("code")) {                                      // code source açık ise bu işlem yapılacak
		richTextEditorjsSummary.execCommand("code", "false");
	}

}

function setDateFormat(date) {
	try {
		if (date.length > 3) {
			var newdate = date.split("/");
			date = newdate[2] + '-' + newdate[0] + '-' + newdate[1];
		}
		else { date = null; }
		return date;

	} catch (e) {
		return null;
	}
}

var secilendeger;
function staticContentPageDuzenle(pid) {
	update = true;
	$('#exampleModalSizeLg').modal('show');
	secilendeger = $('#kt_datatable').DataTable().data().filter(x => x.Id == pid)[0];
	var date = secilendeger.Date;
	var grupId = secilendeger.GrupId;
	var tempId = secilendeger.TempId;

	$("#p_GrupId").val(secilendeger.GrupId).trigger('change');
	$("#p_TempId").val(secilendeger.TempId).trigger('change');
	$('#p_Title').val(secilendeger.Title);
	//$('#p_Summary').val(secilendeger.Summary);
	//$('#p_Html').val(secilendeger.Html);

	(secilendeger.Summary == null) ? richTextEditorjsSummary.setHTMLCode('') : richTextEditorjsSummary.setHTMLCode(secilendeger.Summary);
	if (richTextEditorjsSummary.isCommandActive("code")) {                                      // code source açık ise bu işlem yapılacak
		richTextEditorjsSummary.execCommand("code", "false");
	}
	(secilendeger.Html == null) ? richTextEditorjsHTML.setHTMLCode('') : richTextEditorjsHTML.setHTMLCode(secilendeger.Html);
	if (richTextEditorjsHTML.isCommandActive("code")) {                                      // code source açık ise bu işlem yapılacak
		richTextEditorjsHTML.execCommand("code", "false");
	}


	$("#p_OrderNo").val(secilendeger.OrderNo);
	
	$('#p_Foto1 > .image-input-wrapper').css('background-image', 'url(' + website + 'products/' + secilendeger.Image1 + ')').trigger('change');
	$('#p_Foto2 > .image-input-wrapper').css('background-image', 'url(' + website + 'products/' + secilendeger.Image2 + ')').trigger('change');
	$('#p_Foto3 > .image-input-wrapper').css('background-image', 'url(' + website + 'products/' + secilendeger.Image3 + ')').trigger('change');
	$('#p_Foto4 > .image-input-wrapper').css('background-image', 'url(' + website + 'products/' + secilendeger.Image4 + ')').trigger('change');
	$('#p_Foto5 > .image-input-wrapper').css('background-image', 'url(' + website + 'products/' + secilendeger.Image5 + ')').trigger('change');
	resim1 = secilendeger.Image1;
	resim2 = secilendeger.Image2;
	resim3 = secilendeger.Image3;
	resim4 = secilendeger.Image4;
	resim5 = secilendeger.Image5;

	$('#p_Video').val(secilendeger.Video);
	$('#p_SeoKeywords').val(secilendeger.SeoKeywords);
	$('#p_SeoDesc').val(secilendeger.SeoDesc);
	$('#p_Link').val(secilendeger.Link);
	$("#p_Status").prop('checked', secilendeger.Statu == 1 ? true : false);

	var tarih = setDateFormat(secilendeger.Date);
	$("#p_Date").val(tarih.substr(10, 10));


	//Attachment kısımları
	dosya1 = secilendeger.Attachment1;
	dosya2 = secilendeger.Attachment2;
	dosya3 = secilendeger.Attachment3;

	$('#p_Attachment1').val(secilendeger.Attachment1);
	$('#p_Attachment2').val(secilendeger.Attachment2);
	$('#p_Attachment3').val(secilendeger.Attachment3);

	$(".dropzone-msg1").show();
	$(".dropzone-msg2").show();
	$(".dropzone-msg3").show();

	$(".dz-preview").hide();//?

	$(".dropzone-msg-title1").html(secilendeger.Attachment1);
	$(".dropzone-msg-title2").html(secilendeger.Attachment2);
	$(".dropzone-msg-title3").html(secilendeger.Attachment3);


}

var statusChanger;
function staticContentPageKaydet() {
	statusChanger = 'false';


	$('#exampleModalSizeLg').pleaseWait();
	var staticContentPage = {
		'Id': 0,
		'GrupId': $('#p_GrupId').val(),
		'TempId': $('#p_TempId').val(),
		'Title': $('#p_Title').val(),
		'Summary': richTextEditorjsSummary.getHTMLCode(),
		//'Summary': $('#p_Summary').val(),
		'Html': richTextEditorjsHTML.getHTMLCode(),
		//'Html': myeditor.getData(), /*$('#p_Html').val(),*/
		'OrderNo': ($('#p_OrderNo').val() == '' || $('#p_OrderNo').val() == null) ? 0 : $('#p_OrderNo').val(),
		'Image1': resim1,
		'Image2': resim2,
		'Image3': resim3,
		'Image4': resim4,
		'Image5': resim5,
		'Attachment1': dosya1,
		'Attachment2': dosya2,
		'Attachment3': dosya3,
		'Video': $('#p_Video').val(),
		'SeoKeywords': $('#p_SeoKeywords').val(),
		'SeoDesc': $('#p_SeoDesc').val(),
		'Link': $('#p_Link').val(),
		'Date': $('#p_Date').val(),
		'Statu': $('#p_Status').is(':checked') == true ? 1 : 0,

	};
	var dtt;
	if (update) {
		secilendeger.GrupId = staticContentPage.GrupId;
		secilendeger.TempId = staticContentPage.TempId;
		secilendeger.Title = staticContentPage.Title;
		secilendeger.Summary = staticContentPage.Summary;
		secilendeger.Html = staticContentPage.Html;
		secilendeger.OrderNo = staticContentPage.OrderNo;
		secilendeger.Image1 = staticContentPage.Image1;
		secilendeger.Image2 = staticContentPage.Image2;
		secilendeger.Image3 = staticContentPage.Image3;
		secilendeger.Image4 = staticContentPage.Image4;
		secilendeger.Image5 = staticContentPage.Image5;
		secilendeger.Attachment1 = staticContentPage.Attachment1;
		secilendeger.Attachment2 = staticContentPage.Attachment2;
		secilendeger.Attachment3 = staticContentPage.Attachment3;
		secilendeger.Video = staticContentPage.Video;
		secilendeger.SeoKeywords = staticContentPage.SeoKeywords;
		secilendeger.SeoDesc = staticContentPage.SeoDesc;
		secilendeger.Link = staticContentPage.Link;
		secilendeger.Date = staticContentPage.Date;
		secilendeger.Statu = staticContentPage.Statu;

		dtt = { pro: secilendeger };
	} else {
		dtt = { pro: staticContentPage };
	}


	$.ajax({
		data: dtt,
		dataType: 'json',
		cache: false,
		type: "POST",
		url: '/' + lngg + '/StaticContentPage/CreatedStaticContentPage',
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
					statusChanger = 'true';

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


function staticContentPageSil(pid) {

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
				url: '/' + lngg + '/StaticContentPage/DeleteStaticContentPage',
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


var avatar1 = new KTImageInput('p_Foto1');

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
	resimYukle(1, imageInput, "p_anaSayfaFoto1");
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




var avatar2 = new KTImageInput('p_Foto2');

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
	resimYukle(2, imageInput, "p_Foto2");
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


var avatar3 = new KTImageInput('p_Foto3');

avatar3.on('cancel', function (imageInput) {
	resim3 = "";
	swal.fire({
		title: 'Resim İptal',
		type: 'success',
		buttonsStyling: false,
		confirmButtonText: 'OK',
		confirmButtonClass: 'btn btn-primary font-weight-bold'
	});
});

avatar3.on('change', function (imageInput) {
	resimYukle(3, imageInput, "p_Foto3");
});

avatar3.on('remove', function (imageInput) {
	resim3 = "";
	swal.fire({
		title: 'Resim Silindi',
		type: 'error',
		buttonsStyling: false,
		confirmButtonText: 'OK',
		confirmButtonClass: 'btn btn-primary font-weight-bold'
	});
});

var avatar4 = new KTImageInput('p_Foto4');

avatar4.on('cancel', function (imageInput) {
	resim4 = "";
	swal.fire({
		title: 'Resim İptal',
		type: 'success',
		buttonsStyling: false,
		confirmButtonText: 'OK',
		confirmButtonClass: 'btn btn-primary font-weight-bold'
	});
});

avatar4.on('change', function (imageInput) {
	resimYukle(4, imageInput, "p_Foto4");
});

avatar4.on('remove', function (imageInput) {
	resim4 = "";
	swal.fire({
		title: 'Resim Silindi',
		type: 'error',
		buttonsStyling: false,
		confirmButtonText: 'OK',
		confirmButtonClass: 'btn btn-primary font-weight-bold'
	});
});


var avatar5 = new KTImageInput('p_Foto5');

avatar5.on('cancel', function (imageInput) {
	resim5 = "";
	swal.fire({
		title: 'Resim İptal',
		type: 'success',
		buttonsStyling: false,
		confirmButtonText: 'OK',
		confirmButtonClass: 'btn btn-primary font-weight-bold'
	});
});

avatar5.on('change', function (imageInput) {
	resimYukle(5, imageInput, "p_Foto5");
});

avatar5.on('remove', function (imageInput) {
	resim5 = "";
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
		url: '/' + lngg + '/StaticContentPage/UploderImage',
		type: 'POST',
		success: function (data) {
			if (data != 'false') {
				if (hng == 1) {
					resim1 = data;
				} if (hng == 2) {
					resim2 = data;
				} if (hng == 3) {
					resim3 = data;
				} if (hng == 4) {
					resim4 = data;
				} if (hng == 5) {
					resim5 = data;
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
				resim3 = "";
				resim4 = "";
				resim5 = "";
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


var selectedBoatImages = [];
var rowList = [];
var selectedBoatId;
function showAllImages() {
	/*selectedBoatId = boatId;*/
	rowList = [];
	selectedBoatImages = [];
	/*alert(boatId);*/
	$('#rowImageModal').modal('show');
	/*$('#kt_datatableImage').pleaseWait();*/
	$('#imageListId').empty();

	$.ajax({
		/*data: { selectedBoatId: boatId },*/
		dataType: 'json',
		cache: false,
		type: "POST",
		url: hst5,
		success: function (data) {
			if (!data.hata) {
				/*console.log(data.list);*/
				data.list.forEach(function myFunction(item) {

					//var dataContent = '<div id="imageNo' + item.Id + '" class="listitemClass"><img src="' + website + 'assets/boat/' + item.Path + '" alt="' + item.Path + '"></div>';
					var dataContent = '<div id="imageNo" class="listitemClass"><img src="' + website + 'assets/' + sunucuEntityDosyasi + '/' + item + '" alt="' + item + '"></div>';


					$('#imageListId').append(dataContent);

					/*selectedBoatImages.push(item);*/
				});


			}



		},
		complete: function (data2) {

			/*$('#kt_datatableImage').pleaseWait('stop');*/

		},
		error: function (data2) {

		}
	});

}



