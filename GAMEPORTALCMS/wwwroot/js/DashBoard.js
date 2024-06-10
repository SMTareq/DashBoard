
    var app = new Vue({

        el: '#vc_app',
    data() {
                return {
        TotalCR: '0',
    TotalAOF: '0',
    TotalUser: '0',
    TotalStatus: '0',
    message: 'EBL Migration',
    EblMigra_Category: true,
    EblPoc_Category: false,
    selectedEblEmployee: 'admin@petersengineering.com',
    selectedPIEDepartment: '1',
    portalList: [],
    statusPopulate: [],
    EmployeeInfo: [],
    selectedCategory: '',
    selectedGameType: '0',
    selectedDepartment: '1',
    SelectDocClassType: 'Select From List',
    typeSelected: '',
    games: [],
    gateCategoryList: [],
    searchTerm: '',
    itemG: { },
    isActiveChecked: false,
    datatableOptions: {
        searching: true,
    paging: true,
    lengthChange: false,
    ordering: true,
    info: false,
    responsive: true,
                    },
    filteredGames: [],
    PiE: [],
    BarChartZ: [],
    LineChart: [],
    dataa: [],
    selectedFile: null,
    xlsxLoaded: false,
    jsPDFLoaded: false,
    ListFromDate: null,
    ListToDate: null,
    graphFromDate: null,
    graphToDate: null,

    //WorkSpaceBenner
    WorkSpaceUserList: null,
    WorkSpaceStatuslist: null,
    WorkSpaceAcccountTypeList: null,
    WorkSpeceRecordList: null,

    cards: [

    ],
    currentCardIndex: 0

                   

                };
            },

    computed: {

        // reversedCards() {
        //     return this.cards.slice().reverse();
        // }
    },
    methods: {


        addCard() {

                    if (this.selectedPIEDepartment == "1") {

                        var valuee = '';

    switch (this.selectedCategory) {
                            case 'Status':
    valuee = 'M Status';
    break;
    case 'MCIF':
    valuee = 'M CIF';
    break;
    case 'MPRODUCTTYPE':
    valuee = 'M PRODUCT TYPE';
    break;
    case 'MPRODUCTBranch':
    valuee = 'M PRODUCT Branch';
    break;
    case 'MOWNER':
    valuee = 'M OWNER';
    break;
    case 'MTYPE':
    valuee = 'M TYPE';
    break;
    case 'MUser':
    valuee = 'M User';
    break;
    default:
    valuee = ''; // Return an empty string if no match found
                        }
    const card = {
        title: (valuee) + ' Of EBL Migration',
    visible: true
                        };



    this.cards.push(card);
    this.renderCharts(this.cards.length - 1);
    this.currentCardIndex++;
                    }

    if (this.selectedPIEDepartment == "2") {

         var Poc_valuee = '';

         switch (this.selectedCategory) {
                            case 'Status':
    Poc_valuee = 'Status';
    break;
    case 'MCIF':
    Poc_valuee = 'CIF';
    break;
    case 'PRODUCTTYPE':
    Poc_valuee = ' Product Type';
    break;

    case 'PRODUCTBranch':
    Poc_valuee = ' Product Branch';
    break;
    case 'USER':
    Poc_valuee = 'User';
    break;
    default:
    Poc_valuee = ''; // Return an empty string if no match found
                        }
         const card = {
        title: (Poc_valuee) + ' Of EBL POC',
    visible: true
                        };
         this.cards.push(card);
         this.renderCharts(this.cards.length - 1);
         this.currentCardIndex++;
                    }
                },

    renderCharts(index) {

        // this.cards.forEach((card, index) => {
        //     this.renderPieChart(index);
        //     this.renderBarChart(index);
        // });
        this.renderPieChart(index);
    this.renderBarChart(index);

                },
    renderPieChart(index) {


        PieDepartment = this.selectedPIEDepartment;
    CategoryType = this.selectedCategory;
    const gFromDate = this.graphFromDate;
    const gTodate = this.graphToDate;

    if (CategoryType == null || CategoryType == "undefined" || CategoryType == "") {

        $.notify("Please Select Category", 'error');
                        // alert("Please Select Category");

                    } else {

        helper.get('api/EBL_Migration/PIEChart',
            { Department: PieDepartment, type: CategoryType, Fromdate: gFromDate, Todate: gTodate },
            (response) => {
                this.PiE = [];
                this.PiE = response;
                console.log(response);

                Highcharts.chart('container_Pie_' + index, {
                    chart: {
                        type: 'pie'
                    },
                    title: {
                        text: '',
                        align: 'left'
                    },
                    tooltip: {
                        // valueSuffix: '%'
                    },

                    plotOptions: {
                        series: {
                            allowPointSelect: true,
                            cursor: 'pointer',
                            dataLabels: [{
                                enabled: true,
                                distance: 20
                            }, {
                                enabled: true,
                                distance: -40,
                                format: '{point.percentage:.1f}%',
                                style: {
                                    fontSize: '1.2em',
                                    textOutline: 'none',
                                    opacity: 0.7
                                },
                                filter: {
                                    operator: '>',
                                    property: 'percentage',
                                    value: 10
                                }
                            }]
                        }
                    },
                    series: [
                        {
                            name: 'Total',
                            colorByPoint: true,
                            data: this.PiE
                            //  data: this.currentCard.PiE

                        }
                    ]
                });


            });
                    }

                },
    renderBarChart(index) {



        PieDepartment = this.selectedPIEDepartment;
    CategoryType = this.selectedCategory;
    const gFromDate = this.graphFromDate;
    const gTodate = this.graphToDate;

    if (CategoryType == null || CategoryType == "undefined" || CategoryType == "") {

        $.notify("Please Select Category", 'error');
                        // alert("Please Select Category");

                    } else {

        helper.get('api/EBL_Migration/BarChartForDashBoard',
            { Department: PieDepartment, type: CategoryType, Fromdate: gFromDate, Todate: gTodate },
            (response) => {
                this.BarChartZ = [];
                this.BarChartZ = response;

                Highcharts.chart('container_Bar_' + index, {
                    chart: {
                        type: 'column'
                    },
                    title: {
                        text: '',
                        align: 'left'
                    },
                    xAxis: {
                        categories: [],
                        crosshair: true,
                        accessibility: {
                            description: 'Countries'
                        }
                    },
                    yAxis: {
                        /*min: 0,*/
                        title: {
                            text: ''
                        }
                    },
                    tooltip: {
                        valueSuffix: ' '
                    },
                    plotOptions: {
                        column: {
                            pointPadding: 0.2,
                            borderWidth: 0
                        }
                    },
                    series: this.BarChartZ
                    //series: this.currentCard.BarChartZ
                });

            });
                    }
                },

    SendMain() {
        this.itemG = {};
    $('#mdl_game').modal('show');
                },


    exportToExcel() {
                    if (!this.xlsxLoaded) return; // Check if XLSX library is loaded
    const wb = XLSX.utils.book_new();
    const ws = XLSX.utils.json_to_sheet(this.filteredGames);
    XLSX.utils.book_append_sheet(wb, ws, 'People');
    XLSX.writeFile(wb, 'Eastern_Bank_PLC_Migration.xlsx');
                },

    exportToExcelPOC() {
                    if (!this.xlsxLoaded) return; // Check if XLSX library is loaded
    const wb = XLSX.utils.book_new();
    const ws = XLSX.utils.json_to_sheet(this.filteredGames);
    XLSX.utils.book_append_sheet(wb, ws, 'People');
    XLSX.writeFile(wb, 'Eastern_Bank_PLC_POC.xlsx');
                },

    getSerialNumber(index) {
                    // Add 1 to the index since indexing usually starts from 0
                    return index + 1;
                },

    OnchangeGraph() {
                    if (this.selectedPIEDepartment == "1") {

        this.EblMigra_Category = true;
    this.EblPoc_Category = false;

    this.cards = [];
    //TotalRecordOfPerCanicate
    helper.get('api/WorkSpace/Total_Record_Of_Per_Cabinate',
    {Cabinate_Id: this.selectedPIEDepartment },
                            (response) => {
        this.TotalCR = response.totalCR
                                this.TotalAOF = response.totalAOF
    this.TotalStatus = response.totalStatus
    this.TotalUser = response.totalUser
    this.message = 'EBL Migration'
                            });

                    } else {

        this.EblMigra_Category = false;
    this.EblPoc_Category = true;
    this.cards = [];

    //TotalRecordOfPerCanicate
    helper.get('api/WorkSpace/Total_Record_Of_Per_Cabinate',
    {Cabinate_Id: this.selectedPIEDepartment },
                            (response) => {
        this.TotalCR = response.totalCR
                                this.TotalAOF = response.totalAOF
    this.TotalStatus = response.totalStatus
    this.TotalUser = response.totalUser
    this.message = 'EBL POC'
                            });
                    }
                },

    Onchange() {

                    if (this.selectedDepartment == "1") {

        $('#lblDataClass').text('M Data Class');
    $('#lblStatus').text('M Status');
    // Data Class Populate
    helper.get('api/EBL_Migration/EblDataClassPopulate',
    {DepartmentId: this.selectedDepartment },
                            (response) => {
        this.filteredGames = [];
    this.portalList = [];
    this.portalList = response;
    this.SelectDocClassType = 'Select From List'
                            });

    // Status Populate
    helper.get('api/EBL_Migration/EblStatusPopulate',
    {DepartmentId: this.selectedDepartment },
                            (response) => {
        this.filteredGames = [];
    this.statusPopulate = [];
    this.statusPopulate = response;
    this.selectedGameType = '0'
                                // console.log(response);
                            });
                    }
    if (this.selectedDepartment == "2") {

        $('#lblDataClass').text('Data Class');
    $('#lblStatus').text('Status');
    // Data Class Populate
    helper.get('api/EBL_Migration/EblDataClassPopulate',
    {DepartmentId: this.selectedDepartment },
                            (response) => {
        this.filteredGames = [];
    this.portalList = [];
    this.portalList = response;
    this.SelectDocClassType = 'Select From List'
                            });

    // Status Populate
    helper.get('api/EBL_Migration/EblStatusPopulate',
    {DepartmentId: this.selectedDepartment },
                            (response) => {
        this.filteredGames = [];
    this.statusPopulate = [];
    this.statusPopulate = response;
    this.selectedGameType = '0'
                            });

                    }
                },

    GetEmployeeMailInfo() {
        helper.get('api/EBLLogin/EBLEmployeeInfo',
            {},
            (response) => {
                this.EmployeeInfo = [];
                this.EmployeeInfo = response;
            });
                },

    getDta() {

        PieDepartment = this.selectedPIEDepartment;
    CategoryType = this.selectedCategory;
    const gFromDate = this.graphFromDate;
    const gTodate = this.graphToDate;

    if (CategoryType == null || CategoryType == "undefined" || CategoryType == "") {

        $.notify("Please Select Category", 'error');
                        // alert("Please Select Category");

                    } else {

        helper.get('api/EBL_Migration/PIEChart',
            { Department: PieDepartment, type: CategoryType, Fromdate: gFromDate, Todate: gTodate },
            (response) => {
                this.PiE = [];
                this.PiE = response;
                console.log(response);

                Highcharts.chart('container', {
                    chart: {
                        type: 'pie'
                    },
                    title: {
                        text: '',
                        align: 'left'
                    },
                    tooltip: {
                        // valueSuffix: '%'
                    },

                    plotOptions: {
                        series: {
                            allowPointSelect: true,
                            cursor: 'pointer',
                            dataLabels: [{
                                enabled: true,
                                distance: 20
                            }, {
                                enabled: true,
                                distance: -40,
                                format: '{point.percentage:.1f}%',
                                style: {
                                    fontSize: '1.2em',
                                    textOutline: 'none',
                                    opacity: 0.7
                                },
                                filter: {
                                    operator: '>',
                                    property: 'percentage',
                                    value: 10
                                }
                            }]
                        }
                    },
                    series: [
                        {
                            name: 'Total',
                            colorByPoint: true,
                            data: this.PiE
                        }
                    ]
                });


            });

    helper.get('api/EBL_Migration/BarChartForDashBoard',
    {Department: PieDepartment, type: CategoryType, Fromdate: gFromDate, Todate: gTodate },
                            (response) => {
        this.BarChartZ = [];
    this.BarChartZ = response;

    Highcharts.chart('container_', {
        chart: {
        type: 'column'
                                    },
    title: {
        text: '',
    align: 'left'
                                    },
    xAxis: {
        categories: [],
    crosshair: true,
    accessibility: {
        description: 'Countries'
                                        }
                                    },
    yAxis: {
        min: 0,
    title: {
        text: ''
                                        }
                                    },
    tooltip: {
        valueSuffix: ' '
                                    },
    plotOptions: {
        column: {
        pointPadding: 0.2,
    borderWidth: 0
                                        }
                                    },
    series: this.BarChartZ
                                });

                            });
                    }
                },

    WSUser() {
        helper.get('api/WorkSpace/WorkSpaceBennerUser',
            { Dept: this.selectedPIEDepartment },
            (response) => {

                console.log(response);

                this.WorkSpaceUserList = [];
                this.WorkSpaceUserList = response;

                $('#mdl_WorkSpace_User').modal('show');

            });
                },

    WSStatus() {
        helper.get('api/WorkSpace/WorkSpaceBennerStatus',
            { Dept: this.selectedPIEDepartment },
            (response) => {

                this.WorkSpaceStatuslist = [];
                this.WorkSpaceStatuslist = response;

                $('#mdl_WorkSpace_Status').modal('show');

            });
                },

    WSAccountType() {

        helper.get('api/WorkSpace/WorkSpacebennerAccountType',
            { Dept: this.selectedPIEDepartment },
            (response) => {


                console.log(response);

                this.WorkSpaceAcccountTypeList = [];
                this.WorkSpaceAcccountTypeList = response;

                $('#mdl_WorkSpace_AccountType').modal('show');

            });
                },

    WSAllRecord() {

        helper.get('api/WorkSpace/WorkSpaceTotalRecord',
            { Dept: this.selectedPIEDepartment },
            (response) => {

                console.log(response);

                this.WorkSpeceRecordList = [];
                this.WorkSpeceRecordList = response;

                $('#mdl_WorkSpace_Record').modal('show');

            });
                },

    getData() {

                    const DepartmetId = this.selectedDepartment;
    const gameTypeId = this.selectedGameType;
    const DepartmentId = this.selectedDepartment;
    const DocClass = this.SelectDocClassType;
    const fromdate = this.ListFromDate;
    const todate = this.ListToDate;
    if (DepartmetId == 1) {

        helper.get('api/EBL_Migration/MigrationList',
            { DocClass: DocClass, status: gameTypeId, FromDate: fromdate, Todate: todate },
            (response) => {

                this.filteredGames = [];
                this.filteredGames = response;
            });
                    }

    if (DepartmetId == 2) {

        helper.get('api/EBL_Migration/EBLPOCList',
            { DocClass: DocClass, status: gameTypeId, FromDate: fromdate, Todate: todate },
            (response) => {
                this.filteredGames = [];
                this.filteredGames = response;
            });
                    }
                },

    sendTableDataToBackend() {

                    if (this.filteredGames == null || this.filteredGames == "undefined" || this.filteredGames == "") {

        $.notify("There is no data in the list below", 'error');
    $('#mdl_game').modal('hide');

                    } else {

                        var jsonData = { };

    jsonData["MyProperty"] = this.selectedEblEmployee

    var jsonObjs = [];
    $.each(this.filteredGames, function (index, filteredGames) {

                            var theObj = { };
    // Access properties of each JSON object
    var dataClass = filteredGames.datA_CLASS;
    var Account = filteredGames.accounT_NO;
    var status = filteredGames.status;
    var Date = filteredGames.dwstoredatetime;

    theObj["M_DATA_CLASS"] = dataClass;
    theObj["M_ACCOUNT_NO"] = Account;
    theObj["STATUS"] = status;
    theObj["DWSTOREDATETIME"] = Date;

    jsonObjs.push(theObj);
    jsonData["mailBodies"] = jsonObjs;

                        });

    $.ajax({
        url: '/login/Mailsend',
    type: 'POST',
    data: {
        jsonData: jsonData
                            },
    beforeSend: function () {
        $('#btn_mail').prop('disabled', true);
                            },
    success: function (response) {

                                if (response.success) {
        $('#btn_mail').prop('disabled', false);
    $('#mdl_game').modal('hide');
    $.notify(response.message, 'success');
                                } else {
        $('#btn_mail').prop('disabled', false);
    $.notify(response.message, 'error');
                                }
                            },
    Complete: function () {

        $('#btn_mail').prop('disabled', false);
                            }
                        });
                    }
                },

    openUrl(url) {
        window.open(url, '_blank');
                }
            },
    mounted() {

        //  this.getData();
        helper.blockUI();
    helper.unBlockUI();

    // this.getData();
    const script = document.createElement('script');
    script.src = 'https://cdn.jsdelivr.net/npm/xlsx@0.18.2/dist/xlsx.full.min.js';
                script.onload = () => {
        this.xlsxLoaded = true; // Set flag to true when script is loaded
                };
    document.head.appendChild(script);

    //  this.Onchange();
    this.GetEmployeeMailInfo();

    // __init__();
    helper.get('api/WorkSpace/Total_Record_Of_Per_Cabinate',
    {Cabinate_Id: this.selectedPIEDepartment },
                    (response) => {
        this.TotalCR = response.totalCR
                        this.TotalAOF = response.totalAOF
    this.TotalStatus = response.totalStatus
    this.TotalUser = response.totalUser
                    });

                this.cards.forEach((_, index) => {
        this.renderCharts(index);
                });
            },

        });

