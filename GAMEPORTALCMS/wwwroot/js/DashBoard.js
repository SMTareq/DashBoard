
    var app = new Vue({

        el: '#vc_app',
    data() {
            return {
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
    graphToDate: null
            };
        },

    computed: {

    },
    methods: {

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

    Onchange() {

                if (this.selectedDepartment == "1") {

        // Data Class Populate
        helper.get('api/EBL_Migration/EblDataClassPopulate',
            { DepartmentId: this.selectedDepartment },
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

        // Data Class Populate
        helper.get('api/EBL_Migration/EblDataClassPopulate',
            { DepartmentId: this.selectedDepartment },
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

                Highcharts.chart('container', {
                    chart: {
                        type: 'pie'
                    },
                    title: {
                        text: '',
                        align: 'left'
                    },
                    tooltip: {
                        valueSuffix: '%'
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
                            name: 'Percentage',
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

    this.Onchange();
    this.GetEmployeeMailInfo();
        },

    });



