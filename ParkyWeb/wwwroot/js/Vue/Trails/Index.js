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
            axios.get("/trails/GetAllTrail").then((s) => {
                console.log(s);
                objectData.responseData.list = s.data.data;
                $('#dataTable').DataTable().destroy();
            }).catch((err) => { console.log(err) }).finally(() => { DtInit(); });
        }

        const Delete = (id) => {
            const url = `/trails/Delete/${id}`;
            
            //API
            Swal.fire({
                title: '確定要刪除?',
                text: "動作執行後無法還原!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: 'red',
                //cancelButtonColor: '#d33',
                confirmButtonText: '刪除!'
            }).then((result) => {
                if (result.isConfirmed) {
                    console.log(url);
                    axios.delete(url).then((s) => {
                        console.log(s.data);
                        if (s.data.success) {
                            toastr.success(s.data.message);
                        } else {
                            console.log(s.data);
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
}).mount('#trail_Index');

// #region [表格初始化]
function DtInit() {
    $('#dataTable').DataTable({
        language: { url: "../lib/datatable/CHT.json" },
        //lengthMenu: [[15, 50, -1], [15, 50, "全部"]],
        lengthMenu: [[-1], ["全部"]],
        columnDefs: [
            { "width": "25%", "targets": 0 },
            { "width": "20%", "targets": 1 },
            { "width": "15%", "targets": 2 },
            { "width": "15%", "targets": 3 },
            { "width": "25%", "targets": 4, "orderable": false }
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