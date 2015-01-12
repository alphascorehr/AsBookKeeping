//<![CDATA[



function noUnload() {
    $(window).unbind('beforeunload');
}


function jqConfirm(docType) {
    docAction = docType;
    $("#myForm").validate();
    var v = $("#myForm").valid();

    if (v) {
        var text = "";
        switch (docType) {
            case "genDispatch":
                text = "Da li želite kreirati otpremnicu iz računa br. " + '@Model.UniqueIdentifier' + "?"
                break;
            case "genCopy":
                text = "Da li želite kopirati račun br. " + '@Model.UniqueIdentifier' + "?"
                break;
        }
        $('#dialogtext').text(text);
        $("#dialog").dialog("open");
    }
}



function ShowDocDiv() {
    var div = $('#GeneratedDocsDiv');
    var link;
    if (id == 0) { $('#CreateDocCopyImg').hide(); }
    if (dispatched == "True") {
        $('#invDiv').show();
        link = url + "../Dispatch/CreateAndEdit/" + dispatchedId;
        $('#dispatch').attr('href', link);
        $('#CreateDocDispatchImg').hide();
        div.fadeIn('slow');
    }
    
    if (coppied == "True" || action.indexOf("_") != -1) {
        if (action.indexOf("_") != -1) { coppiedId = action.split("_")[1]; }

        $('#copyDiv').show();
        link = url + "CreateAndEdit/" + coppiedId;
        $('#copy').attr('href', link);
        div.fadeIn('slow');
    }
}

function CreateDoc(docType) {
    //$("#myForm").validate();
    //var v = $("#myForm").valid();

    // if (v) {
    if ((AreControlsModified() || isGridModified)) {
        $('#HiddenValueAction').attr('value', docType);
        document.myForm.submit();
    }
    else {
        var Id = parseInt($('#Id').attr('value'));
        var actionURL = url + "CreateDoc";
        $.post(actionURL, { FromDocumentId: Id, docType: docType }, function (result) {
            if (docType == 'genDispatch') {
                dispatchedId = result;
                dispatched = 'True';
                ShowDocDiv();
            }
            else {
                coppied = "True";
                coppiedId = result;
                ShowDocDiv();
            }
        });
    }
    //}
}



$(".delete").live('click', function (event) {
    $(this).parent().parent().remove();
});

/* f-cija prebacena dole */
//    function OnStartCallback(s, e) {
//        isCallback = false;
//        refreshAvansPayment = false;
//        EnableProductIdChanged = false;
//        if (e.command == "DELETEROW" || e.command == "UPDATEEDIT") {
//            refreshAvansPayment = true;
//        }
//        if (e.command == "DELETEROW" || e.command == "CANCELEDIT") {
//            isCallback = true;
//        }

//        if (e.command == "ADDNEWROW" || e.command == "STARTEDIT") {
//            //$('#gvDocumentItemsCol_DXEditor0').css({ 'border': '0' })
//            $('#SubjectId_I').attr('readonly', true); // ne radi
//            $('#CourseValue_I').attr('readonly', true);
//        }
//        else {
//            $('#SubjectId_I').attr('readonly', false); // ne radi
//            $('#CourseValue_I').attr('readonly', false);
//        }
//        return false;
//    }
function OnInit(s, e) {
    var actionURL =  url + "Calc";
    $.post(actionURL, { isCallback: true }, function (result) {
        $('#Ukupno').html(result[0]);
        $('#Pdv').html(result[1]);
        $('#ZaPlatiti').html(result[2]);
        $('#Rabat').html(result[3]);
    });
    return false;
}

function UseForClosingValueChanged(s, keyValue, url) {
    //        var val1 = parseFloat(retailValue);
    //        var val2 = parseFloat(payedTillDocument);
    var data = { key: keyValue };
    if (s.GetValueString() != null)
        data.value = s.GetValueString();

    $.post(url, data, function (result) {
        if (result == true) {
            gvFreeAvansesGrid.PerformCallback();
        }
    });

}

//1-Proizvod
//3-Cijena
//4-JM
//5-Kolicina
//6-rabat %
//8-Porezna stopa
//10-Jed.Cijena
//11-Ukupno
//    

function ProductIdChanged(s, e) {
    var ProizvodId = parseInt($('#gvDocumentItemsCol_DXEditor2_VI').attr('value'));
    gvDocumentItemsCol_DXEditor4.SetValue(""); /* Cijena */
    gvDocumentItemsCol_DXEditor5.SetValue(""); /* JM */
    gvDocumentItemsCol_DXEditor6.SetValue("1"); /* Kolicina */
    gvDocumentItemsCol_DXEditor7.SetValue("0"); /* Rabat */
    gvDocumentItemsCol_DXEditor9.SetValue(""); /* Porezna stopa */
    gvDocumentItemsCol_DXEditor11.SetValue(""); /* Jed.cijena */
    gvDocumentItemsCol_DXEditor12.SetValue(""); /* Ukupno */

    var Tec = $('#CourseValue_I').attr('value');
    if (Tec == null || Tec == "") {
        Tec = 1;
    }


    if (ProizvodId != null && $('#SubjectId_VI').attr('value') != null && $('#SubjectId_VI').attr('value') != 0 && $('#SubjectId_VI').attr('value') != NaN) {
        var actionURL =  url + "ProductIdChanged";
        $.post(actionURL, { proizvodId: ProizvodId, tecaj: Tec }, function (result) {
            gvDocumentItemsCol_DXEditor4.inputElement.value = result[0]
            gvDocumentItemsCol_DXEditor11.inputElement.value = result[0];  /* po defaultu stavi jed. cijenu kao cijenu (rabat je po defaultu 0%) */
            //$('#Cijena').attr('value', result[3]);
            //Tecaj();
            Ukupno(); /* odmah izracunaj ukupnu cijenu */
            if (result[1] != 0) {
                gvDocumentItemsCol_DXEditor5.SetValue(result[1]); /* Jm */
            }
            if (result[2] != 0) {
                gvDocumentItemsCol_DXEditor9.SetValue(result[2]); /* porez % */
            }
            Calc();
        });

    }
    else {
        alert("Molimo, odaberite klijenta");
        gvDocumentItemsCol.CancelEdit();
    }


}

function TecajToHiddenVal() {
    var tecaj = parseFloat($('#CourseValue_I').attr('value').replace(',', '.'));
    if (tecaj == "" || tecaj == null) {
        tecaj = 1;
    }
    $('#prevTecaj').attr('value', tecaj);
}

function Tecaj() {
    var tecaj = document.getElementById("CourseValue_I").value
    var t = parseFloat($('#CourseValue_I').attr('value').replace(',', '.'));


    var prevTecaj = parseFloat($('#prevTecaj').attr('value').replace(',', '.'));

    if (t != "" && t != null && t != NaN && !isNaN(t)) {
        var actionURL =  url + "TecajChanged";
        $.post(actionURL, { Tecaj: tecaj, PrevTecaj: prevTecaj }, function (result) {
            gvDocumentItemsCol.Refresh();
            $('#Ukupno').html(result[0]);
            $('#Pdv').html(result[1]);
            $('#ZaPlatiti').html(result[2]);
            $('#Rabat').html(result[3]);
        });


    }

}


function Calc() {
    var ordinal = $('#gvDocumentItemsCol_DXEditor1_I').attr('value');
    var taxId = $('#gvDocumentItemsCol_DXEditor9_VI').attr('value');
    var wholeSaleprice = $('#gvDocumentItemsCol_DXEditor12_I').attr('value');
    //var rabat = $('#gvDocumentItemsCol_DXEditor6_I').attr('value').replace('%', '');
    var rabat = parseFloat($('#gvDocumentItemsCol_DXEditor7_I').attr('value').replace('%', '').replace(',', '.'));
    // maloprodajna cijena
    var cijena = $('#gvDocumentItemsCol_DXEditor4_I').attr('value');
    var kolicina = $('#gvDocumentItemsCol_DXEditor6_I').attr('value');
    if (ordinal == null || ordinal == NaN || ordinal == "") {
        ordinal = 0;
    }
    var Tec = $('#CourseValue_I').attr('value');
    if (Tec == null || Tec == "") {
        Tec = 1;
    }
    if (taxId != null && taxId != "") {
        var actionURL = url + "Calc";
        $.post(actionURL, { IsCallback: false, Ordinal: ordinal, TaxId: taxId, WholeSalePrice: wholeSaleprice, Cijena: cijena, Kolicina: kolicina, Rabat: rabat, Tecaj: Tec }, function (result) {
            $('#Ukupno').html(result[0]);
            $('#Pdv').html(result[1]);
            $('#ZaPlatiti').html(result[2]);
            $('#Rabat').html(result[3]);
        });
    }
}

function SubjectIdChanged(s, e) {
    /* SubjectId se trpa u Session */
    var SubjectId = parseInt($('#SubjectId_VI').attr('value'));

    if (SubjectId == null || SubjectId == "" || SubjectId == "0" || isNaN(SubjectId)) {
        return false;
    }
    $('#AdresaKlijentaDiv').slideUp('slow');
    if (SubjectId != null) {
        var actionURL =  url + "SubjectIdChanged";
        $.post(actionURL, { subjectId: SubjectId }, function (result) {
            var oib = result[0];
            var adresa = result[1];
            var mjesto = result[2];
            if (oib == null) {
                oib = "Nije unešen";
            }
            if (adresa == null) {
                adresa = "Nije unešena";
            }
            if (mjesto == null) {
                mjesto = "Nije unešeno";
            }
            $('#oib').html("OIB: " + oib)
            $('#ulica').html("Adresa: " + adresa);
            $('#mjesto').html("Mjesto: " + mjesto);

            if (BillToAddress_PlaceId == "" && BillToAddress_Street == "") {
                var billToAddr = result[3];
                var billToPlaceName = result[4];
                var billToPlaceId = result[5];
                if (billToPlaceId != 0 && billToPlaceName != null) {
                    BillToAddress_PlaceId.AddItem(billToPlaceName, billToPlaceId);
                    BillToAddress_PlaceId.SetText(billToPlaceName);
                }
                if (billToAddr != null) {
                    BillToAddress_Street.SetValue(billToAddr);
                }
                //if (billToPlaceId != null || billToPlaceName != null || billToAddr != null) {
                if (billToPlaceName != "") {
                    $('#cBoxBillTo').attr('checked', true);
                    $("#BillToDiv").slideDown('slow');
                }
            }
            else {
                $('#cBoxBillTo').attr('checked', true);
                $("#BillToDiv").slideDown('slow');
            }
        });
        $('#AdresaKlijentaDiv').slideDown('slow');

        gvFreeAvansesGrid.PerformCallback();
    }
    //gvDocumentItemsCol.CancelEdit();
    //ProductIdChanged();
    return SubjectId;
}

function JedCijena(s, e) {
    var Rabat = parseFloat($('#gvDocumentItemsCol_DXEditor7_I').attr('value').replace(',', '.'));
    var Cijena = parseFloat($('#gvDocumentItemsCol_DXEditor4_I').attr('value').replace(',', '.'));
    if (Rabat != null && Cijena != null && Cijena != NaN) {
        gvDocumentItemsCol_DXEditor11.inputElement.value = (Cijena - (Cijena / 100 * Rabat)).toString().replace(".", ",");
        Ukupno();
    }
}

function Ukupno(s, e) {
    var kol = parseFloat($('#gvDocumentItemsCol_DXEditor6_I').attr('value').replace(',', '.'));
    var jedCijena = parseFloat($('#gvDocumentItemsCol_DXEditor11_I').attr('value').replace(',', '.'));

    if (kol != null && jedCijena != null && kol != NaN && jedCijena != NaN && kol != undefined) {
        gvDocumentItemsCol_DXEditor12.inputElement.value = parseFloat(kol * jedCijena).toFixed(2).toString().replace(".", ","); /* Ukupno(WholeSalePrice) */
    }
    Calc();
}

function gvPaymentSelectionChanged(s, e) {

}

function gvPaymentFocusedRowChanged(s, e) {

}

function MjestaComboDropDown(s, e) {
    if (selectedName != "") {

    }

}


function MjestaComboValidation(s, e) {
    if (e.value == null || e.value == "")
        return;

    var mjestoId = Number(e.value);
    if (isNaN(mjestoId))
        e.isValid = false;
}

//    function Print() {

//        $('.footer').hide();
//        $('#DefaultEmements').slideToggle('slow');
//        $('#printDiv').slideToggle('slow');
//        $('#printImg').hide();
//        $('#mailImg').hide();
//        

//        $('#printLink').attr('onclick', 'ClosePrint()');
//        $('#printImg').attr('title', 'Zatvori ispis dokumenta');
//        $('#printImg').attr('src', '@Url.Content("~/Content/images/close.png")');
//        $('#printImg').fadeIn('slow');
//    }

//    function ClosePrint() {
//        $('#DefaultEmements').slideToggle('slow');
//        $('#printDiv').slideToggle('slow');

//        $('#printImg').hide();
//        
//        $('#printImg').attr('src', '@Url.Content("~/Content/images/Printer_Blue48.png")');
//       

//        $('#printLink').attr('onclick', 'Print()');
//        $('#printImg').attr('title', 'Ispiši dokument');
//        $('#printImg').fadeIn('slow');
//        $('#mailImg').fadeIn('slow');

//        $('.footer').show();
//    }


/*
        Step 3. - 
*/

function GetValues() {
    var list = new Array(SubjectId, BillToAddress_Street, BillToAddress_PlaceId, ShipToAddress_Street, ShipToAddress_PlaceId, PayedInFull, DocumentDateTime, MaturityDate, DispatchDate, MDGeneral_Enums_CurrencyId, CourseValue, DispatchCompanyId, Conditions, DispatchDescription, Description);
    var array = new Array();
    for (i = 0; i <= list.length - 1; i++) {
        //var val = $('#' + list[i]).attr('value');
        var val = list[i];
        array[i] = val.GetValue();
    }

    return array;
}

isGridModified = false;
function Print() {
    $("#myForm").validate();
    var v = $("#myForm").valid();

    if (v) {
        if (AreControlsModified() || isGridModified) {
            $('#HiddenValueAction').attr('value', 'print');
            document.myForm.submit();
        }
        else {
            ShowPrint();
        }
    }
}

function AreControlsModified() {
    var newValues = new Array();
    newValues = GetValues();

    for (i = 0; i <= newValues.length - 1; i++) {
        if (initValues[i] != newValues[i]) {
            return true;
            break;
        }
    }
    return false;
}
isCallback = false;
function OnStartCallback(s, e) {
    /* check if grid has been modified */
    if (e.command == "DELETEROW" || e.command == "UPDATEEDIT" || e.command == "ADDNEWROW") {
        isGridModified = true;
    }

    if (e.command == "DELETEROW" || e.command == "CANCELEDIT") {
        isCallback = true;
    }

    refreshAvansPayment = false;
    EnableProductIdChanged = false;
    if (e.command == "DELETEROW" || e.command == "UPDATEEDIT") {
        refreshAvansPayment = true;
    }
    if (e.command == "DELETEROW" || e.command == "CANCELEDIT") {
        isCallback = true;
    }
    if (e.command == "STARTEDIT" || e.command == "ADDNEWROW") {
        gridInEdit = true;
        $('.button_large').attr('onclick', '');
        $('#genDispatch').attr('onclick', '');
        $('#genCopy').attr('onclick', '');
        $('#printLink').attr('onclick', '');
    }
    if (e.command == "CANCELEDIT" || e.command == "UPDATEEDIT") {
        gridInEdit = false;
        $('.button_large').attr('onclick', '$(this).closest("form").submit()');
        $('#genDispatch').attr('onclick', 'jqConfirm("genDispatch")');
        $('#genCopy').attr('onclick', 'jqConfirm("genCopy")');
        $('#printLink').attr('onclick', 'Print()');
        $('#addNevDocumentItem').show()
    }
}
function OnEndCallback(s, e) {
    $('#gvDocumentItemsCol_DXEditor0').css({ 'border': '0' })
    if (isCallback) {
        var actionURL = url + "Calc";
        $.post(actionURL, { isCallback: true }, function (result) {
            $('#Ukupno').html(result[0]);
            $('#Pdv').html(result[1]);
            $('#ZaPlatiti').html(result[2]);
            $('#Rabat').html(result[3]);
        });
        isCallback = false;
    }
    if (gridInEdit) {
        $('#addNevDocumentItem').hide();
    }

    if (refreshAvansPayment) {
        gvFreeAvansesGrid.PerformCallback();
        refreshAvansPayment = false;
    }
    return false;
}
function ShowPrint() {
    var actionUrl = url + "PrintDocument";
    //var id = '@Model.Id';
    $.post(actionUrl, { id: id }, function (result) {
        //if (result == true) {
        $('.footer').hide();
        $('#DefaultEmements').slideToggle('slow');
        $('#printDiv').slideToggle('slow');
        $('#printImg').hide();
        $('#mailImg').hide();

        $('#printLink').attr('onclick', 'ClosePrint()');
        $('#printImg').attr('title', 'Zatvori ispis dokumenta');
        $('#printImg').attr('src', close);
        $('#printImg').fadeIn('slow');
        ReportViewer1.Refresh();
        //}
        //else {
        //    alert("Greška prilikom kreiranja dokumenta za ispis!");
        //}
    })

}

function ClosePrint() {
    $('#DefaultEmements').slideToggle('slow');
    $('#printDiv').slideToggle('slow');

    $('#printImg').hide();

    $('#printImg').attr('src', printer);

    $('#printLink').attr('onclick', 'Print()');
    $('#printImg').attr('title', 'Ispiši dokument');
    $('#printImg').fadeIn('slow');
    $('#mailImg').fadeIn('slow');

    $('.footer').show();
}




function Mail() {
    $('#mailErr').hide();
    $('#mailOk').hide();
    var sub = "Račun br. " + UniqueIdentifier;

    var SubjectId = parseInt($('#SubjectId_VI').attr('value'));
    if (SubjectId == null || SubjectId == "" || SubjectId == "0") {
        return false;
    }
    //        var actionURL = '@Url.Content("~/Documents/Invoice/FindEmailById")';
    //        $.post(actionURL, { subjectId: SubjectId }, function (result) {
    //            SendTo.SetValue(result);
    Subject.SetValue(sub);
    //        });
    $('.footer').hide();
    $('#DefaultEmements').slideToggle('slow');
    $('#mailDiv').slideToggle('slow');
    $('#printImg').hide();
    $('#mailImg').hide();


    $('#mailLink').attr('onclick', 'CloseMail()');
    $('#mailImg').attr('title', 'Zatvori slanje dokumenta');
    $('#mailImg').attr('src', close);
    $('#mailImg').fadeIn('slow');
}

function CloseMail() {
    $('#DefaultEmements').slideToggle('slow');
    $('#mailDiv').slideToggle('slow');

    $('#mailImg').hide();

    $('#mailImg').attr('src', send);


    $('#mailLink').attr('onclick', 'Mail()');
    $('#mailImg').attr('title', 'Pošalji dokument');
    $('#printImg').fadeIn('slow');
    $('#mailImg').fadeIn('slow');

    $('.footer').show();
}

function SendReportByEmail() {
    //Wait();
    $('#mailErr').hide();
    $('#mailOk').hide();
    var Id = parseInt($('#Id').attr('value'));
    if (Id == null || Id == "" || Id == "0") {
        return false;
    }

    var sendTo = SendTo.GetValue();
    var sendToCC = SendToCC.GetValue();
    var subject = Subject.GetValue();
    var body = Body.GetValue();
    var clientName = $('#SubjectId_I').attr('value');
    if (clientName == null || clientName == "" || clientName == "0") {
        return false;
    }
    /* Id, SentTo, SendToCC, Subject, Body */
    var actionURL = url + "SendReportByEmail";
    $.post(actionURL, { Id: Id, SendTo: sendTo, SendToCC: sendToCC, Subject: subject, Body: body, ClientName: clientName }, function (result) {
        if (result == "Ok") {
            $('#mailErr').hide();
            $('#mailOk').fadeIn('slow');
            $('#mailOkLabel').html(SendTo.GetValue());
        }
        else {
            $('#mailOk').hide();
            $('#mailErr').fadeIn('slow');
            $('#mailErrLabel').html(result.toString());
        }
        //$("*").css("cursor", "auto");
    });

}

function Wait() {
    $("*").css("cursor", "wait");
}

function addClientDiv() {
    $('#addClientDiv').slideToggle();
    $('#addClientDivHide').slideToggle();
    $('#desniDiv').slideToggle();
    if ($('#lblAddClient').text() == "Zatvori") {
        $('#lblAddClient').text('Dodaj klijenta');
    }
    else {
        $('#lblAddClient').text('Zatvori');
    }

}

function showClientPopup() {
    ClientName.SetValue("");
    ClientOIB.SetValue("");
    SubjectType.SetValue("");
    Mjesta3.SetValue("");
    UlicaBroj.SetValue("");
    pcModalModeCli.Show();
    Mjesta3.PerformCallback();
}

function AddClient() {
    var ClName = ClientName.GetValue();
    var ClOIB = ClientOIB.GetValue();
    var SubType = SubjectType.GetValue();
    var MjestoId = Mjesta3.GetValue();
    var Ulicabroj = UlicaBroj.GetValue();
    $("#myFormCli").validate();
    var v = $("#myFormCli").valid();

    if (v) {
        var actionUrl = urlProduct + "AddClient";
        $.post(actionUrl, { Name: ClName, OIB: ClOIB, SubjectType: SubType, Mjesto: MjestoId, UlicaBroj: Ulicabroj }, function (result) {
            var id = parseInt(result);
            //var id = 1000;
            if (id > 0) {
                SubjectId.AddItem(ClName, id);
                SubjectId.SetText(ClName);
                pcModalModeCli.Hide();
                SubjectIdChanged();
            }
        });
    }
}

function showProductPopup() {
    ProductName.SetValue("");
    TaxRateId.SetValue("");
    WholesalePrice.SetValue("");
    Label.SetValue("");
    UnitId.SetValue("");
    pcModalMode.Show();
}
function showServicePopup() {
    ServiceName.SetValue("");
    ServiceTaxRateId.SetValue("");
    ServiceLabel.SetValue("");
    WholesalePriceSrv.SetValue("");
    ServiceUnitId.SetValue("");
    pcModalModeSrv.Show();
}


function AddProduct() {
    var Name = ProductName.GetValue();
    var Tax = TaxRateId.GetValue();
    var WSprice = WholesalePrice.GetValue();
    var Lbl = Label.GetValue();
    var Unit = UnitId.GetValue();

    $("#myFormPro").validate();
    var v = $("#myFormPro").valid();

    if (v) {
        //if (Name != null && Tax != null && WSprice != null && Label != null && Unit != null) {
        var actionUrl =  urlProduct + "AddProduct";
        $.post(actionUrl, { Name: Name, Tax: Tax, WSprice: WSprice, Label: Lbl, Unit: Unit }, function (result) {
            var id = parseInt(result);
            //var id = 1000;
            if (id > 0) {
                gvDocumentItemsCol.CancelEdit();
                pcModalMode.Hide()
            }
        });
    }
}

function AddService() {
    var Name = ServiceName.GetValue();
    var Tax = ServiceTaxRateId.GetValue();
    var Label = ServiceLabel.GetValue();
    var Wsprice = WholesalePriceSrv.GetValue();
    var Unit = ServiceUnitId.GetValue();
    $("#myFormSrv").validate();
    var v = $("#myFormSrv").valid();

    if (v) {
        //if (Name != null && Tax != null && Label != null && Wsprice != null && Unit != null) {
        var actionUrl = urlService + "AddService";
        $.post(actionUrl, { Name: Name, Tax: Tax, Wsprice: Wsprice, Unit: Unit }, function (result) {
            var id = parseInt(result);
            //var id = 1000;
            if (id > 0) {
                gvDocumentItemsCol.CancelEdit();
                pcModalModeSrv.Hide()
            }
        });
    }

}

var idGl;
function GetDesc(id) {
    idGl = id;
    var actionUrl = url + "GetDesc";
    $.post(actionUrl, { id: id }, function (result) { DescriptionDesc.SetValue(result) });
    pcModalModeDesc.Show();
}

function AddDesc() {
    var actionUrl = url + "AddDesc";
    var memo = DescriptionDesc.GetText();
    $.post(actionUrl, { id: idGl, desc: memo }, function (result) {
        pcModalModeDesc.Hide();
        gvDocumentItemsCol.Refresh();
    });
}

function PopupSubjectTypeChanged() {
    var subType = SubjectType.GetValue();
    if (subType == 0) {
        $('#Oibdiv').removeClass('required');
        //$('#ClientOIB').rules("remove", "required");

    }
    else {
        $('#Oibdiv').addClass('required');
        //$('#ClientOIB').rules("add", "required");

    }
}
// ]]>