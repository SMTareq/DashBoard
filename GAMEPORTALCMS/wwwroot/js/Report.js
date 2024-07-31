/// <reference path="../vue/excel.js" />

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

    SelectAccountNo: '',
    SelectProductBranch: '',
    SelectCIF: '',
    SelectProductType: '',

    ProductTypePopulate: [],
    CIFList: [],
    AccountNoPopulate: [],

    typeSelected: '',

    columns: ['documentName', 'datA_CLASS', 'accounT_NO', 'productType', 'branchCode', 'cif', 'status'], // Initial columns

    draggedIndex: null,

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
        visibleColumns() {
                    // Filter out columns that are not visible
                    return this.columns.filter(column => this.isColumnVisible(column));
                }
            },
    methods: {

        exportToPDF() {

                    const {jsPDF} = window.jspdf;

    // let doc = new jsPDF('l', 'mm', [1450, 1350]);

    const doc = new jsPDF();
    const table = document.querySelector('table');
    const columns = this.visibleColumns;
    const rows = [];

                    // Add table headers
                    const headers = columns.map(column => ({title: column, dataKey: column }));
                    rows.push(headers.reduce((acc, cur) => ({...acc, [cur.dataKey]: cur.title }), { }));

                    // Add table rows
                    this.filteredGames.forEach(row => {
                        const rowData = { };
                        columns.forEach(column => {
        rowData[column] = row[column];
                        });
    rows.push(rowData);
                    });

    doc.autoTable({
        head: [headers.map(header => header.title)],
                        body: rows.map(row => columns.map(column => row[column])),
                    });

    // Save PDF file
    doc.save('exported_data.pdf');
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

    Onchange() {

                    // const DeptId = $("#DepartmentId").val();

                    if (this.selectedDepartment == "1") {

        // this.reloadColumns();

        $('#lblAccountNo').text('M Account No');
    $('#lblProductType').text('M Product Type');
    $('#lblProductBranch').text('M Product Branch');
    $('#lblCIF').text('M CIF');
    $('#lblStatus').text('M Status');

    // Document Name Populate
    helper.get('api/EBL_Migration/EblAccountNoPopulate',
    {DepartmentId: this.selectedDepartment },
                            (response) => {
        this.filteredGames = [];
    this.AccountNoPopulate = [];
    this.AccountNoPopulate = response;
    this.SelectAccountNo = 'Select From List'
                            });

    // Product Type Populate
    helper.get('api/EBL_Migration/EblProductTypePopulate',
    {DepartmentId: this.selectedDepartment },
                            (response) => {
        this.filteredGames = [];
    this.ProductTypePopulate = [];
    this.ProductTypePopulate = response;
    this.SelectProductType = 'Select From List'
                            });

    // Data Class Populate
    helper.get('api/EBL_Migration/EblBranchCodePopulate',
    {DepartmentId: this.selectedDepartment },
                            (response) => {
        this.filteredGames = [];
    this.portalList = [];
    this.portalList = response;
    this.SelectProductBranch = 'Select From List'
                            });

    // Status Populate
    helper.get('api/EBL_Migration/EblCIFPopulate',
    {DepartmentId: this.selectedDepartment },
                            (response) => {
        this.filteredGames = [];
    this.CIFList = [];
    this.CIFList = response;
    this.SelectCIF = 'Select From List'
                                // console.log(response);
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

        // this.reloadColumns();

        $('#lblAccountNo').text('Account No');
    $('#lblProductType').text('Product Type');
    $('#lblProductBranch').text('Product Branch');
    $('#lblCIF').text('CIF');
    $('#lblStatus').text('Status');

    // Document Name Populate
    helper.get('api/EBL_Migration/EblAccountNoPopulate',
    {DepartmentId: this.selectedDepartment },
                            (response) => {
        this.filteredGames = [];
    this.AccountNoPopulate = [];
    this.AccountNoPopulate = response;
    this.SelectAccountNo = 'Select From List'
                            });

    // Data Class Populate
    helper.get('api/EBL_Migration/EblBranchCodePopulate',
    {DepartmentId: this.selectedDepartment },
                            (response) => {
        this.filteredGames = [];
    this.portalList = [];
    this.portalList = response;
    this.SelectProductBranch = 'Select From List'
                            });

    // Product Type Populate
    helper.get('api/EBL_Migration/EblProductTypePopulate',
    {DepartmentId: this.selectedDepartment },
                            (response) => {
        this.filteredGames = [];
    this.ProductTypePopulate = [];
    this.ProductTypePopulate = response;
    this.SelectProductType = 'Select From List'
                            });

    // Status Populate
    helper.get('api/EBL_Migration/EblCIFPopulate',
    {DepartmentId: this.selectedDepartment },
                            (response) => {
        this.filteredGames = [];
    this.CIFList = [];
    this.CIFList = response;
    this.SelectCIF = 'Select From List'
                                // console.log(response);
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

    getData() {

                    const DepartmentId = $("#DepartmentId").val();
    // const gameTypeId = this.selectedGameType;
    const gameTypeId = $("#Status").val();

    const AccountNo = $("#Acc").val();
    const fromdate = this.ListFromDate;
    const todate = this.ListToDate;

    // const ProductBranch = this.SelectProductBranch;
    // const ProductType = this.SelectProductType;
    const ProductBranch = $("#ProductBranch").val();
    const ProductType = $("#ProductType").val();

    // const CIF = this.SelectCIF;
    const CIF = $("#CIF").val();

    if (DepartmentId == 1) {

        helper.get('api/EBL_Migration/MigrationList',
            { AccountNo: AccountNo, status: gameTypeId, FromDate: fromdate, Todate: todate, ProductBranch: ProductBranch, ProductType: ProductType, CIF: CIF },
            (response) => {

                //  this.reloadColumns();

                this.filteredGames = [];
                this.filteredGames = response;
                this.selectedDepartment = DepartmentId;
                this.SelectAccountNo = AccountNo;
                this.SelectProductType = ProductType;
                this.SelectProductBranch = ProductBranch;
                this.SelectCIF = CIF;
                this.selectedGameType = gameTypeId

            });
                    }

    if (DepartmentId == 2) {

        helper.get('api/EBL_Migration/EBLPOCList',
            { AccountNo: AccountNo, status: gameTypeId, FromDate: fromdate, Todate: todate, ProductBranch: ProductBranch, ProductType: ProductType, CIF: CIF },
            (response) => {

                //this.reloadColumns();

                this.filteredGames = [];
                this.filteredGames = response;
                this.selectedDepartment = DepartmentId;
                this.SelectAccountNo = AccountNo;
                this.SelectProductType = ProductType;
                this.SelectProductBranch = ProductBranch;
                this.SelectCIF = CIF;
                this.selectedGameType = gameTypeId

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

    var DocumentName = filteredGames.documentName
    var BranchCode = filteredGames.branchCode
    var ProductType = filteredGames.productType
    var cif = filteredGames.cif


    theObj["M_DATA_CLASS"] = dataClass;
    theObj["M_ACCOUNT_NO"] = Account;
    theObj["STATUS"] = status;
    theObj["DWSTOREDATETIME"] = Date;

    theObj["DocumentName"] = DocumentName;
    theObj["BranchCode"] = BranchCode;
    theObj["ProductType"] = ProductType;
    theObj["Cif"] = cif;


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
                },

    getSerialNumber(index) {
                    // Add 1 to the index since indexing usually starts from 0
                    return index + 1;
                },

    //
    dragStart(index) {
        this.draggedIndex = index;
                },
    drop(index) {
                    const draggedColumn = this.columns[this.draggedIndex];
    this.columns.splice(this.draggedIndex, 1); // Remove from old position
    this.columns.splice(index, 0, draggedColumn); // Insert at new position
                },
    addColumn() {
                    const newColumn = prompt('Enter column name:');
    if (newColumn) {
        this.columns.push(newColumn);
                    }
                },
    removeColumn(index) {
        this.columns.splice(index, 1);
                },
    reloadColumns() {
        // Reset columns to initial state

        // this.columns: ['documentName', 'datA_CLASS', 'accounT_NO', 'productType', 'branchCode', 'cif', 'status'];

    },
    isColumnVisible(column) {
                    // Implement logic to determine if a column is visible or not
                    // For now, assume all columns are visible
                    return true;
                },
    exportToExcel() {
                    // Filter data based on visible columns
                    const filteredData = this.filteredGames.map(row => {
                        const filteredRow = { };
                        this.visibleColumns.forEach(column => {
        filteredRow[column] = row[column];
                        });
    return filteredRow;
                    });

    // Create worksheet
    const ws = XLSX.utils.json_to_sheet(filteredData);

    // Create workbook
    const wb = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, "Sheet1");

    // Save workbook to file
    XLSX.writeFile(wb, 'exported_data.xlsx');
                },
    formatHeader(header) {
                    return header
    .replace(/_/g, ' ')
                        .replace(/(?:^\w|[A-Z]|\b\w)/g, (word, index) => index === 0 ? word.toUpperCase() : word)
    .replace(/\s+/g, ' ');
                }

            },
    mounted() {

        helper.blockUI();

                // this.$nextTick(() => {
        //     console.log("USb");
        //     $('#gameTable').DataTable();
        // });

        this.$nextTick(() => {
            $('.select2').select2();
        });
    //excel loading
    const script = document.createElement('script');
        /*script.src = 'https://cdn.jsdelivr.net/npm/xlsx@0.18.2/dist/xlsx.full.min.js';*/

        script.src = '~/vue/excel.js';

                script.onload = () => {
        this.xlsxLoaded = true; // Set flag to true when script is loaded
                };
    document.head.appendChild(script);

    this.Onchange();
    this.GetEmployeeMailInfo();
    helper.unBlockUI();

            },

        });
