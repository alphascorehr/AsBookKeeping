
$.validator.addMethod('decimalNumberNull', function (value) {
    return (/^[0-9]+(\,[0-9]{1,2})?$/.test(value) || value == null || value == "");
}, 'Unesite broj na dvije decimale');


$.validator.addMethod('decimalNumber', function (value) {
    return /^[0-9]+(\,[0-9]{1,2})?$/.test(value);
}, 'Unesite broj na dvije decimale');

    var offerId;
    //var link = 'http://silver.demo.local/alpha/';
    var link = 'http://localhost/AlphaWebCommodityBookkeeping/';
        function BlockUI() {
            $.blockUI({
                message: '<h2>Samo trenutak...</h2>',
                fadeIn: 700,
                css: {
                    left: ($(window).width() - 220) / 2 + 'px',
                    width: '200px',
                    border: 'none',
                    padding: '15px',
                    backgroundColor: '#000',
                    '-webkit-border-radius': '10px',
                    '-moz-border-radius': '10px',
                    opacity: .5,
                    color: '#fff'
                }
            });
        }

        function UnblockUI() {
            $.unblockUI({
                fadeOut: 700
            });
        }
        function LogInMob() {
            $('#myForm').validate({
                rules: {
                    userName: "required",
                    password: "required"
                },
                messages: {
                    userName: "Unesite ime",
                    password: "Unesite lozinku"
                }
            });
            $('#myForm').validate().element('#userName');
            $('#myForm').validate().element('#password');
            if ($('#userName').valid() && $('#password').valid()) {
                BlockUI();
                var usName = $('#userName').attr('value');
                var pass = $('#password').attr('value');
                $.post(link + 'Account/MobileLogIn', { UserName: usName, Password: pass }, function (result) {
                    if (result[0] == 'True') {
                        $('#lblPassError').text('');
                        getOffers();
                        window.location.replace("#List");
                    }
                    else {
                        var mess = result[1];
                        $('#lblPassError').text(mess);
                    }
                    UnblockUI();
                });
            }
        }

        function LogOff() {
            BlockUI();
            $.post(link + 'Account/MobileLogOff', function (result) {
                window.location.replace("#Login");
                UnblockUI();
            });
        }
        var listId = new Array();
        var listRbr = new Array();
        var listKlijent = new Array();
        var listDatum = new Array();
        var listValjanost = new Array();
        var listIznos = new Array();
        var page = 10;
        var index = 0;
        function getOffers() {
            var items = "";
            $('#previous').hide();
            $.post(link + "MobileOffer/GetOfferList", function (result) {
                for (var i = 0; i < result.length; i++) {
                    listId[i] = result[i][0];
                    listRbr[i] = result[i][1];
                    listKlijent[i] = result[i][2];
                    listDatum[i] = result[i][3];
                    listValjanost[i] = result[i][4];
                    listIznos[i] = result[i][5];
                    if (i <= page - 1) {
                        items += "<li><a href='#' onclick=EditOffer(" + result[i][0] + ")><table width='100%'><tr><td width='15%'>" + result[i][1] + "</td></td><td width='50%'>" + result[i][2] + "</td><td width='10%'>" + result[i][3] + "</td><td width='10%'>" + result[i][4] + "</td><td width='15%' align='right'>" + result[i][5] + "</td></tr></table></a></li>"
                    }
                }
                $("#OfferList").append(items);
                $("#OfferList").listview('refresh');
                if (index + page > result.length) {
                    $('#next').hide();
                }
                else { $('#next').show(); }
            });
        }
        function Next() {
            $('#OfferList').children().remove();
            var items = "";
            total = listId.length;
            for (i = index + page; i < index + page + page; i++) {
                var Id = listId[i];
                var Rbr = listRbr[i];
                var Klijent = listKlijent[i];
                var Datum = listDatum[i];
                var Valjanost = listValjanost[i];
                var Iznos = listIznos[i];


                if (Id != undefined) {
                    items += "<li><a href='#' onclick=EditOffer(" + Id + ")><table width='100%'><tr><td width='15%'>" + Rbr + "</td></td><td width='50%'>" + Klijent + "</td><td width='10%'>" + Datum + "</td><td width='10%'>" + Valjanost + "</td><td width='15%' align='right'>" + Iznos + "</td></tr></table></a></li>"
                }
            }
            AddHeaderOfferList();
            $('#OfferList').append(items);
            $('#OfferList').listview('refresh');
            index = index + page;
            $('#previous').show();
            if (index + page > total) {
                $('#next').hide();
            }
        }
        function Previous() {
            var items = "";
            $('#OfferList').children().remove();
            total = listId.length;
            for (i = index - page; i < index; i++) {
                var Id = listId[i];
                var Rbr = listRbr[i];
                var Klijent = listKlijent[i];
                var Datum = listDatum[i];
                var Valjanost = listValjanost[i];
                var Iznos = listIznos[i];

                if (Id != undefined) {
                    items += "<li><a href='#' onclick=EditOffer(" + Id + ")><table width='100%'><tr><td width='15%'>" + Rbr + "</td></td><td width='50%'>" + Klijent + "</td><td width='10%'>" + Datum + "</td><td width='10%'>" + Valjanost + "</td><td width='15%' align='right'>" + Iznos + "</td></tr></table></a></li>"
                }

            }
            index = index - page;
            AddHeaderOfferList();
            $('#OfferList').append(items);
            $('#OfferList').listview('refresh');
            if (index - page < 0) {
                $('#previous').hide();
            }
            $('#next').show();
        }
        function AddHeaderOfferList() {
            $('#OfferList').append('<li><table width="100%"><tr><td width="15%"><b>Rbr</b></td><td width="50%"><b>Klijent</b></td><td width="10%"><b>Datum</b></td><td width="10%"><b>Valjanost</b></td><td align="right" width="15%"><b>Iznos&nbsp;&nbsp;&nbsp;</b></td></tr></table></li>');
        }
        function TodayDate() {
            var myDate = new Date();
            var prettyDate = myDate.getDate() + '.' + (myDate.getMonth() + 1) + '.' + myDate.getFullYear() + ".";
            return prettyDate;
        }
        function NewOffer() {
            offerId = 0;
            $("#StavkeUlDiv").hide();
            window.location.replace("#CreateAndEdit");
            GetSubjectList(0);
            GetDispatchCompanyList(0);
            GetDispatchTypeList(0);
            //GetCurrencyList();

            $('#UkupnoFull').html("0");
            $('#lblPopust').html("0");
            $('#lblUkupnoSaPop').html("0");
            $('#lblPdv').html("0");
            $('#lblZaPlatiti').html("0");

            $('#valjanostPonude').attr('value', TodayDate());
            $('#datumPonude').attr('value', TodayDate());
            $('#datumIsporuke').attr('value', TodayDate());
            $('#valuta').attr('value', "");
            $('#StavkeUl').children().remove();
            dodajHeader();
        }
        var SubjectIdGl;
        function EditOffer(id) {
            offerId = id;
            $("#StavkeUlDiv").show();
            window.location.replace("#CreateAndEdit");
            
            //GetCurrencyList();
            $.post(link + "MobileOffer/GetOffer", { id: id }, function (result) {
                var subjectId = result.Item1[0];
                SubjectIdGl = subjectId;
                var matDate = result.Item1[1];
                var dispTypeId = result.Item1[2];
                var docDateTime = result.Item1[3];
                var dispDate = result.Item1[4];
                var dispCompanyId = result.Item1[5];

                var UkupnoObracun = result.Item1[6];
                var UkupnoFull = result.Item1[7];
                var rabatFull = result.Item1[8];
                var pdv = result.Item1[9];
                var ZaPlatiti = result.Item1[10];
                //                var myselect = $("select#SubjectId");
                //                myselect.val(subjectId);
                //                myselect.selectmenu("refresh");

                //                var myselect1 = $("select#nacinIsporuke");
                //                myselect1.val(dispTypeId);
                //                myselect1.selectmenu("refresh");

                //                var myselect2 = $("select#prijevoznik");
                //                myselect2.val(dispCompanyId);
                //                myselect2.selectmenu("refresh");

                GetSubjectList(subjectId);
                GetDispatchCompanyList(dispCompanyId);
                GetDispatchTypeList(dispTypeId);

                $('#valjanostPonude').attr('value', matDate);
                $('#datumPonude').attr('value', docDateTime);
                $('#datumIsporuke').attr('value', dispDate);





                //Stavke
                $("#StavkeUl").children().remove();
                dodajHeader();
                var items = "";
                var args;
                for (var i = 0; i < result.Item2.length; i++) {
                    var id = result.Item2[i][0];
                    var ordinal = result.Item2[i][1];
                    var proizvod = result.Item2[i][2];
                    var cijena = result.Item2[i][3];
                    var jm = result.Item2[i][4];
                    var kolicina = result.Item2[i][5];
                    var rabat = result.Item2[i][6];
                    var poreznaStopa = result.Item2[i][7];
                    var jedCijena = result.Item2[i][8];
                    var ukuono = result.Item2[i][9];

                  
                    args = id + ',\'' + proizvod + '\'';
                    items += '<li><a href="#" onclick="CreateAndEditStavka(' + args + ')"><table width="100%"><tr><td width="5%">' + ordinal + '</td><td width="35%">' + proizvod + '</td><td width="10%">' + cijena + '</td><td width="10%">' + jm + '</td><td width="10%">' + kolicina + '</td><td width="10%">' + rabat + '</td><td width="10%">' + poreznaStopa + '</td><td width="10%">' + ukuono + '</td></tr></table></a></li>'
                }
                $('#StavkeUl').append(items);
                $("#StavkeUl").listview('refresh');

                $('#UkupnoFull').html(UkupnoFull);
                $('#lblPopust').html(rabatFull);
                $('#lblUkupnoSaPop').html(UkupnoObracun);
                $('#lblPdv').html(pdv);
                $('#lblZaPlatiti').html(ZaPlatiti);
            });
        }

        function dodajHeader() {
            $("#StavkeUl").append('<li><table width="100%"><tr><td width="5%"><b>Rbr</b></td><td width="35%"><b>Proizvod</b></td><td width="10%"><b>Cijena</b></td><td width="10%"><b>Jm</b></td><td width="10%"><b>Količina</b></td><td width="10%"><b>Rabat</b></td><td width="10%"><b>Por.stopa</b></td><td width="10%"><b>Ukupno</b></td></tr></table></li>');
        }

        function GetSubjectList(subjectId) {
            var items = '<option value=""></option>';
            if ($('#SubjectId').html() == "") {
                $.post(link + "MobileOffer/GetSubjectsListFull", function (result) {
                    for (var i = 0; i < result.length; i++) {
                        items += '<option value="' + result[i].Item1 + '">' + result[i].Item2 + '</option>';
                    }
                    $('#SubjectId').html(items);
                    var myselect = $("select#SubjectId");
                    myselect.val(subjectId);
                    myselect.selectmenu("refresh");
                }

            );
            }
            else {
                var myselect = $("select#SubjectId");
                myselect.val(subjectId);
                myselect.selectmenu("refresh");
            }
        }
        function GetDispatchCompanyList(id) {
            var items = '<option value=""></option>';
            if ($('#prijevoznik').html() == "") {
                $.post(link + "MobileOffer/GetDispatchCompanyList", function (result) {
                    for (var i = 0; i < result.length; i++) {
                        items += '<option value="' + result[i].Item1 + '">' + result[i].Item2 + '</option>';
                    }
                    $('#prijevoznik').html(items);
                    var myselect = $("select#prijevoznik");
                    myselect.val(id);
                    myselect.selectmenu("refresh");
                });
            }
            else {
                var myselect = $("select#prijevoznik");
                myselect.val(id);
                myselect.selectmenu("refresh");
            }
        }
        function GetDispatchTypeList(id) {
            var items = '<option value=""></option>';
            if ($('#nacinIsporuke').html() == "") {
                $.post(link + "MobileOffer/GetDispatchTypeList", function (result) {
                    for (var i = 0; i < result.length; i++) {
                        items += '<option value="' + result[i].Item1 + '">' + result[i].Item2 + '</option>';
                    }
                    $('#nacinIsporuke').html(items);
                    var myselect = $("select#nacinIsporuke");
                    myselect.val(id);
                    $('#nacinIsporuke').selectmenu("refresh");
                });
            }
            else {
                var myselect = $("select#nacinIsporuke");
                myselect.val(id);
                $('#nacinIsporuke').selectmenu("refresh");
            }
        }
//        function GetCurrencyList() {
//            var items = '<option value=""></option>';
//            $.post(link + "MobileOffer/GetCurrencyList", function (result) {
//                for (var i = 0; i < result.length; i++) {
//                    items += '<option value="' + result[i].Item1 + '">' + result[i].Item2 + '</option>';
//                }
//                $('#valuta').html(items);
//                $('#valuta').selectmenu("refresh");
//            });
//        }

        function saveOffer() {
            var subjectId = $('#SubjectId').val();
            var datumPonude = $('#datumPonude').attr('value');
            var valjanostPonude = $('#valjanostPonude').attr('value');
            var datumIsporuke = $('#datumIsporuke').attr('value');
            //var Select1 = $('#Select1').val();
            //var valuta = $('valuta').attr('value');
            var nacinIsporuke = $('#nacinIsporuke').val();
            var prijevoznik = $("#prijevoznik").val();

            $("#SubjectId").rules("remove", "required");
            $("#SubjectId").rules("add", {
                required: true,
                messages: {
                    required: "Odaberite klijenta"
                }
            });
            $('#myForm').validate().element('#SubjectId');
            if ($('#SubjectId').valid()) {

                $.post(link + "MobileOffer/SaveOffer", { id: offerId, SubjectId: subjectId, docDate: datumPonude, matDate: valjanostPonude, datIsporuke: datumIsporuke, nacinIsporuke: nacinIsporuke, prijevoznik: prijevoznik }, function (result) {
                    if (result.Item1 == true) {
                        $('#StavkeUlDiv').show();
                        offerId = result.Item2;
                    }

                });
            }
        }
        var PriceFromPriceList;
        var PriceListId;
        var StavkaId;
        function CreateAndEditStavka(id, proizvod) {
            StavkaId = id;
            window.location.replace("#CreateAndEditStavka");
            $("#lblProizvod").rules("remove", "required");
            $("#selectJm").rules("remove", "required");
            $("#selectPorStopa").rules("remove", "required");
            $("#tboxKolicina").rules("remove", "required");
            $("#tboxCijena").rules("remove", "required");
            $("#tboxRabat").rules("remove", "required");
            $("#tboxRabat").rules("remove", "decimalNumberNull");
            $("#tboxKolicina").rules("remove", "decimalNumberNull");
            $("#tboxCijena").rules("remove", "decimalNumberNull");
            if (proizvod == 0) {
                $('#lblProizvod').html("Odaberite");
                $('#lblProizvod').attr('value', "");
                $('#tboxCijena').attr('value', "")
                $('#tboxKolicina').attr('value', "1");
                $('#tboxRabat').attr('value', "0");
                $('#lblUkupno').html("0")
                //$('#selectJm').val()
                GetUnitList(0);
                GetTax(0);
            }
            else {
                $.post(link + 'MobileOffer/GetItem', { id: id }, function (result) {
                    var ordinal = result[0];
                    var productId = result[1];
                    var price = result[2];
                    var priceFromPriceList = result[3];
                    var priceListId = result[4];
                    var unitId = result[5];
                    var quantity = result[6];
                    var rabatePercentage = result[7];
                    var taxRateId = result[8];
                    var ammount = result[9]; // jed cijena
                    var wholesaleValue = result[10];

                    PriceFromPriceList = priceFromPriceList;
                    PriceListId = priceListId;
                    GetUnitList(unitId);
                    GetTax(taxRateId);
                    $('#lblProizvod').html(proizvod);
                    $('#lblProizvod').attr('value', productId);
                    $('#tboxCijena').attr('value', price)
                    $('#tboxKolicina').attr('value', quantity);
                    $('#tboxRabat').attr('value', rabatePercentage);
                    $('#lblUkupno').html(wholesaleValue);
                    $('#lblJedCijena').html(ammount);
                });
                
            }
        }
        var index = 0;
        function GetProductList(who) {
            $('#GetProductListUl').children().remove();

            if (who == 'btn') {
                index = 0;
            }

            var search = $('#GetProductListSearch').attr('value');
//            if (search.length < 2) {
//                return;
//            }
            $.post(link + 'MobileOffer/GetEntityList', { index: index, search: search }, function (result) {
                var options = '';
                if (result.length > 0) {
                    if (result.length == 10) {
                        $('#btnGetMoreProductList').show();
                    }
                    else {
                        $('#btnGetMoreProductList').hide();
                    }


                    for (var i = 0; i < result.length; i++) {
                        var arg = result[i].Item1 + ',\'' + result[i].Item2 + '\'';
                        $('#GetProductListUl').append('<li><a href="#CreateAndEditStavka" onclick="SetSelectedproduct(' + arg + ');">' + result[i].Item2 + '</a></li>');
                    }
                    $('#GetProductListUl').listview('refresh');
                }
                else {
                    $('#btnGetMoreProductList').hide();
                    $('#lblSearch').html('Nema razultata');
                }
                
                //UnblockUI();
            });

        }

        function GetMoreProductList() {
            index = index + 10;
            $('#btnGetPreviousProductList').show();
            GetProductList('fn');
        }

        function GetPreviousProductList() {
            if (index > 10) {
                index = index - 10;
                $('#btnGetPreviousProductList').show();
            }
            else {
                index = 0;
                $('#btnGetPreviousProductList').hide();
            }
            GetProductList('fn');
        }

        var ProizvodId;
        function SetSelectedproduct(productMaterialId, productMaterialName) {
            $('#lblProizvod').attr('value', productMaterialName);
            $('#lblProizvod').html(productMaterialName);
            ProizvodId = productMaterialId
//            $("#lblProizvod").rules("remove", "required");
//            $("#lblProizvod").rules("add", {
//                required: true,
//                messages: {
//                    required: "Odaberite proizvod"
//                }
//            });
          
            //ProductMaterialId = productMaterialId;
            //$('#myForm').validate().element('#lblRad');
            $.post(link + 'MobileOffer/GetProductData', { subjectId: SubjectIdGl, productId: productMaterialId }, function (result) {
                var price = result[0];
                var unitId = result[1];
                var taxrateId = result[2];
                var pricelistId = result[3];

                PriceFromPriceList = price;
                PriceListId = pricelistId;

                $('#tboxCijena').attr('value', price)
                //$('#lblUkupno').html(wholesaleValue);
                GetUnitList(unitId);
                GetTax(taxrateId);

                Ukupno();
                $('#myForm').validate().element('#lblProizvod');
                
                $('#myForm').validate().element('#tboxKolicina');
                $('#myForm').validate().element('#tboxCijena');
                $('#myForm').validate().element('#tboxRabat');
            });
          
        }

        function Ukupno() {
            var jedCijena = Djubre('tboxCijena') - (Djubre('tboxCijena') * Djubre('tboxRabat') / 100);
            var ukupno = jedCijena * Djubre('tboxKolicina');
            ukupno = ukupno.toFixed(2);
            $('#lblUkupno').html(ukupno);
            //(Cijena - (Cijena / 100 * Rabat))
            var pdvPerc = parseFloat($('#selectPorStopa option:selected').text());
            
            jedCijena = jedCijena.toFixed(2);
            $('#lblJedCijena').html(jedCijena);
            //alert(parseFloat($('#tboxCijena').attr('value').replace(".", "").replace(",", ".")));
        }


        function Djubre(input) {
            var out = parseFloat($('#' + input).attr('value').replace(".", "").replace(",", "."));
            var out1 = out.toFixed(2);

            return out1;
        }

//        function roundNumber(num, dec) {
//            var result = Math.round(num * Math.pow(10, dec)) / Math.pow(10, dec);
//            return result;
//        }

        function GetUnitList(id) {
            var items = '<option value=""></option>';
            if ($('#selectJm').html() == "") {
                $.post(link + "MobileOffer/GetUnitsList", function (result) {
                    for (var i = 0; i < result.length; i++) {
                        items += '<option value="' + result[i].Item1 + '">' + result[i].Item2 + '</option>';
                    }
                    $('#selectJm').html(items);
                    var myselect = $("select#selectJm");
                    myselect.val(id);
                    
                    $('#selectJm').selectmenu("refresh");
                    $('#myForm').validate().element('#selectJm');
                });
            }
            else {
                var myselect = $("select#selectJm");
                myselect.val(id);
                $('#selectJm').selectmenu("refresh");
                $('#myForm').validate().element('#selectJm');
            }
        }
        function GetTax(id) {
            var items = '<option value=""></option>';
            if ($('#selectPorStopa').html() == "") {
                $.post(link + "MobileOffer/GetTaxsList", function (result) {
                    for (var i = 0; i < result.length; i++) {
                        items += '<option value="' + result[i].Item1 + '">' + (parseFloat(result[i].Item3)).toFixed(2) + '</option>';
                    }
                    $('#selectPorStopa').html(items);
                    var myselect = $("select#selectPorStopa");
                    myselect.val(id);
                    $('#selectPorStopa').selectmenu("refresh");
                    $('#myForm').validate().element('#selectPorStopa');
                });
            }
            else {
                var myselect = $("select#selectPorStopa");
                myselect.val(id);
                $('#selectPorStopa').selectmenu("refresh");
                $('#myForm').validate().element('#selectPorStopa');
            }
        }


        function SaveStavka() {
            //var productId = $('#lblProizvod').attr('value');
            var productId = ProizvodId;
            var unitId = $('#selectJm').val();
            var taxRateId = $('#selectPorStopa').val();
            var quantity = $('#tboxKolicina').val();
            var ammount = $('#lblJedCijena').text();
            var price = $('#tboxCijena').val();
            var wholesaleValue = $('#lblUkupno').text();
            var rabatePercentage = $('#tboxRabat').val();

            $("#lblProizvod").rules("remove", "required");
            $("#selectJm").rules("remove", "required");
            $("#selectPorStopa").rules("remove", "required");
            $("#tboxKolicina").rules("remove", "required");
            $("#tboxCijena").rules("remove", "required");
            $("#tboxRabat").rules("remove", "required");
            $("#lblProizvod").rules("add", {
                required: true,
                messages: {
                    required: "Obavezno polje"
                }
            });
          
            $("#selectPorStopa").rules("add", {
                required: true,
                messages: {
                    required: "Obavezno polje"
                }
            });
            $("#tboxKolicina").rules("add", {
                required: true,
                decimalNumberNull: true,
                messages: {
                    required: "Obavezno polje"
                }
            });

            $("#tboxRabat").rules("add", {
                required: true,
                decimalNumberNull: true,
                messages: {
                    required: "Obavezno polje"
                }
            });
            $("#tboxCijena").rules("add", {
                required: true,
                decimalNumberNull: true,
                messages: {
                    required: "Obavezno polje"
                }
            });

//            $("#selectJm").rules("add", {
//                required: true,
//                messages: {
//                    required: "Obavezno polje"
//                }
//            });
            $('#myForm').validate().element('#lblProizvod');
            $('#myForm').validate().element('#selectJm');
            $('#myForm').validate().element('#selectPorStopa');
            $('#myForm').validate().element('#tboxKolicina');
            $('#myForm').validate().element('#tboxCijena');
            $('#myForm').validate().element('#tboxRabat');
            if ($('#lblProizvod').valid() && $('#selectJm').valid() && $('#selectPorStopa').valid() && $('#tboxKolicina').valid() && $('#tboxRabat').valid()) {

                $.post(link + 'MobileOffer/SaveItem', { Id: StavkaId, offerId: offerId, productId: productId, unitId: unitId, taxRateId: taxRateId, quantity: quantity, ammount: ammount, price: price, wholesaleValue: wholesaleValue, priceFromPriceList: PriceFromPriceList, priceListId: PriceListId, rabatePercentage: rabatePercentage }, function (result) {
                    var ok = result.Item1;
                    var id = result.Item2;
                    if (ok == true) {
                        StavkaId = id;
                        window.location.replace("#CreateAndEdit");
                        EditOffer(offerId);
                    }
                });
            }
        }

        function DeleteStavka() {
            $.post(link + "MobileOffer/DeleteItem", { id: StavkaId, offerId: offerId }, function (result) {
                if (result == true) {
                    window.location.replace("#CreateAndEdit");
                    EditOffer(offerId);
                }
            });
        }