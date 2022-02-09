const { ref, reactive, onMounted, createApp } = Vue;
createApp({
    setup() {
        const objectData = reactive({
            token: "",
            datePeriod: {
                start: "",
                end: ""
            },
            responseData: {
                list: []
            },
            dto: {}
        });

        const fetchData = () => {
            axios.get("/nationalParks/GetAllNationalPark").then((s) => {
                console.log(s);
                objectData.responseData.list = s.data.data;
                $('#dataTable').DataTable().destroy();
            }).catch((err) => { console.log(err) }).finally(() => { DtInit(); });
        }

        const Delete = (id) => {
            const url = `/nationalParks/Upsert/${id}`;
            console.log(id);
            return;
            //API
            Swal.fire({
                title: 'Are you sure?',
                text: "You won't be able to revert this!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                //cancelButtonColor: '#d33',
                confirmButtonText: 'Delete!'
            }).then((result) => {
                if (result.isConfirmed) {
                    console.log(url);
                    axios.delete(url).then((s) => {
                        console.log(s.data);
                        if (s.data.success) {
                            toastr.success(s.data.message);
                        } else {

                        }
                        fetchData();
                    }).catch((err) => { console.log(err) });
                }
            });
        }

        onMounted(() => {
            fetchData();
        });

        return {
            objectData,
            Delete
        }
    }
}).mount('#nationalPark_Index');

// #region [表格初始化]
function DtInit() {
    $('#dataTable').DataTable({
        language: { url: "../lib/datatable/CHT.json" },
        //lengthMenu: [[15, 50, -1], [15, 50, "全部"]],
        lengthMenu: [[-1], ["全部"]],
        columnDefs: [
            { "width": "50%", "targets": 0 },
            { "width": "30%", "targets": 1 },
            { "width": "20%", "targets": 2, "orderable": false }
        ],
        dom: "ftip",
        //ordering: false,
        deferRender: true,
        autoWidth: false,
        destroy: true,
        retrieve: true,
        initComplete: function () { },
        footerCallback: function () { }
    });
}
// #endregion