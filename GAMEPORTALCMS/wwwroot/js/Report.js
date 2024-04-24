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

            gateCategoryList: [],
            searchTerm: '',
            itemG: {},
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

        exportToPDF() {
            // Options for the PDF export
            const options = {
                filename: 'people.pdf',
                image: { type: 'jpeg', quality: 0.98 },
                html2canvas: {},
                jsPDF: { unit: 'pt', format: 'a4', orientation: 'portrait' },
            };

            // Perform the PDF export
            this.$htmlToPdf.convert('#appp', options);


            // const doc = new jsPDF({
            //     orientation: "landscape",
            //     unit: "in",
            //     format: [4, 2]
            // });

            // doc.text("Hello world!", 1, 1);
            // doc.save("two-by-four.pdf");
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

            if (this.selectedDepartment == "1") {

                $('#lblAccountNo').text('M Account No');
                $('#lblProductType').text('M Product Type');
                $('#lblProductBranch').text('M Product Branch');
                $('#lblCIF').text('M CIF');
                $('#lblStatus').text('M Status');

                // Document Name Populate
                helper.get('api/EBL_Migration/EblAccountNoPopulate',
                    { DepartmentId: this.selectedDepartment },
                    (response) => {
                        this.filteredGames = [];
                        this.AccountNoPopulate = [];
                        this.AccountNoPopulate = response;
                        this.SelectAccountNo = 'Select From List'
                    });

                // Product Type Populate
                helper.get('api/EBL_Migration/EblProductTypePopulate',
                    { DepartmentId: this.selectedDepartment },
                    (response) => {
                        this.filteredGames = [];
                        this.ProductTypePopulate = [];
                        this.ProductTypePopulate = response;
                        this.SelectProductType = 'Select From List'
                    });

                // Data Class Populate
                helper.get('api/EBL_Migration/EblBranchCodePopulate',
                    { DepartmentId: this.selectedDepartment },
                    (response) => {
                        this.filteredGames = [];
                        this.portalList = [];
                        this.portalList = response;
                        this.SelectProductBranch = 'Select From List'
                    });

                // Status Populate
                helper.get('api/EBL_Migration/EblCIFPopulate',
                    { DepartmentId: this.selectedDepartment },
                    (response) => {
                        this.filteredGames = [];
                        this.CIFList = [];
                        this.CIFList = response;
                        this.SelectCIF = 'Select From List'
                        // console.log(response);
                    });

                // Status Populate
                helper.get('api/EBL_Migration/EblStatusPopulate',
                    { DepartmentId: this.selectedDepartment },
                    (response) => {
                        this.filteredGames = [];
                        this.statusPopulate = [];
                        this.statusPopulate = response;
                        this.selectedGameType = '0'
                        // console.log(response);
                    });
            }
            if (this.selectedDepartment == "2") {

                $('#lblAccountNo').text('Account No');
                $('#lblProductType').text('Product Type');
                $('#lblProductBranch').text('Product Branch');
                $('#lblCIF').text('CIF');
                $('#lblStatus').text('Status');

                // Document Name Populate
                helper.get('api/EBL_Migration/EblAccountNoPopulate',
                    { DepartmentId: this.selectedDepartment },
                    (response) => {
                        this.filteredGames = [];
                        this.AccountNoPopulate = [];
                        this.AccountNoPopulate = response;
                        this.SelectAccountNo = 'Select From List'
                    });

                // Data Class Populate
                helper.get('api/EBL_Migration/EblBranchCodePopulate',
                    { DepartmentId: this.selectedDepartment },
                    (response) => {
                        this.filteredGames = [];
                        this.portalList = [];
                        this.portalList = response;
                        this.SelectProductBranch = 'Select From List'
                    });

                // Product Type Populate
                helper.get('api/EBL_Migration/EblProductTypePopulate',
                    { DepartmentId: this.selectedDepartment },
                    (response) => {
                        this.filteredGames = [];
                        this.ProductTypePopulate = [];
                        this.ProductTypePopulate = response;
                        this.SelectProductType = 'Select From List'
                    });

                // Status Populate
                helper.get('api/EBL_Migration/EblCIFPopulate',
                    { DepartmentId: this.selectedDepartment },
                    (response) => {
                        this.filteredGames = [];
                        this.CIFList = [];
                        this.CIFList = response;
                        this.SelectCIF = 'Select From List'
                        // console.log(response);
                    });

                // Status Populate
                helper.get('api/EBL_Migration/EblStatusPopulate',
                    { DepartmentId: this.selectedDepartment },
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

                var jsonData = {};

                jsonData["MyProperty"] = this.selectedEblEmployee

                var jsonObjs = [];

                $.each(this.filteredGames, function (index, filteredGames) {

                    var theObj = {};
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
        },

        getSerialNumber(index) {
            // Add 1 to the index since indexing usually starts from 0
            return index + 1;
        },

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
        script.src = 'https://cdn.jsdelivr.net/npm/xlsx@0.18.2/dist/xlsx.full.min.js';
        script.onload = () => {
            this.xlsxLoaded = true; // Set flag to true when script is loaded
        };
        document.head.appendChild(script);

        this.Onchange();
        this.GetEmployeeMailInfo();
        helper.unBlockUI();

    },

});