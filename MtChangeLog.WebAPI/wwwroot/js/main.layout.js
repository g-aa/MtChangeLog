let getMainLayout = function () {
    let result = {
        rows: [
            {
                view: "toolbar",
                id: "mainToolbar_id",
                padding: 3,
                elements: [
                    { 
                        // view:"button", 
                        // type:"image",
                        // image:"../icons/icons_table_x48.png",
                        // autowidth:true,
                        // css:"webix_transparent",
                        // tooltip:"Таблицы компонентов",
                        view: "icon", 
                        icon: "mdi mdi-menu", 
                        click: function() {  
                            $$("mainSidebar_id").toggle();
                        }
                    },
                    {
                        view: "label",
                        align: "center",
                        label: "<span style='font-size:30px;'>БМРЗ - change log</span>"
                    }
                ]
            },
            {
                cols: [
                    {
                        view: "sidebar",
                        width:280,
                        id: "mainSidebar_id",
                        data: getMainSidebarMenu(),
                        on: {
                            onItemClick: function (id) {
                                try {
                                    messageBox.information("Был выбран: \'" + this.getItem(id).value + "\'");
                                    let dLayout = $$("dataLayout_id");
                                    switch (id) {
                                        case "analogModuleTableLayout_id":
                                            if(analogModuleLayout == undefined || analogModuleLayout == null) { 
                                               var analogModuleLayout = new AnalogModuleTableLayout(dLayout);
                                            }
                                            analogModuleLayout.show();
                                            break;
                                        case "platformTableLayout_id":
                                            if(platformLayout == undefined || platformLayout == null) {
                                               var platformLayout = new PlatformTableLayout(dLayout);
                                            }
                                            platformLayout.show();
                                            break;
                                        case "prjVersTableLayout_id":
                                            if(projectLayout == undefined || projectLayout == null) {
                                                var projectLayout = new ProjectTableLayout(dLayout);
                                            }
                                            projectLayout.show();
                                            break;
                                        case "prjRevTableLayout_id":
                                            if(projectLayout == undefined || projectLayout == null) {
                                                var projectLayout = new ProjectRevisionLayout(dLayout);
                                            }
                                            projectLayout.show();
                                            break;
                                        case "armEditTableLayout_id":
                                            if(armEditLayout == undefined || armEditLayout == null) {
                                                var armEditLayout = new ArmEditTableLayout(dLayout);
                                            }
                                            armEditLayout.show();
                                            break;
                                        case "authorTableLayout_id":
                                            if(authorLayout == undefined || authorLayout == null) {
                                                var authorLayout = new AuthorTableLayout(dLayout);
                                            }
                                            authorLayout.show();
                                            break;
                                        case "communicationTableLayout_id":
                                            if(comLayout == undefined || comLayout == null) {
                                                var comLayout = new CommunicationTableLayout(dLayout);
                                            }
                                            comLayout.show();
                                            break;
                                        case "relayAlgorithmTableLayout_id":
                                            if(algorithmLayout == undefined || algorithmLayout == null) {
                                                var algorithmLayout = new RelayAlgorithmTableLayout(dLayout);
                                            }
                                            algorithmLayout.show();
                                            break;
                                        case "projectTrees_id":
                                            if(treeLayout == undefined || treeLayout == null) {
                                                var treeLayout = new ProjectTreeLayout(dLayout);
                                            }
                                            treeLayout.show();
                                            break;
                                        case "prohectHistory_id":
                                            if(historyLayout == undefined || historyLayout == null){
                                                var historyLayout = new ProjectHistoryLayout(dLayout);
                                            }
                                            historyLayout.show();
                                            break;
                                        default:
                                            messageBox.warning("Увы, функционал пока не поддерживается!");
                                    }
                                } catch(error){
                                    messageBox.error(error.message);
                                }
                            }
                        }
                    },
                    {
                        view: "layout",
                        id: "dataLayout_id",
                        rows: [
                            {
                                view: "template",
                                template: "Default template with some text inside"
                            }
                        ]
                    }
                ]
            }
        ]
    };
    return result;
};

// основное меню:
let getMainSidebarMenu = function() {
    let result = [
        {
            id:"analogModuleTableLayout_id",
            icon:"mdi mdi-table",
            value:"таблица аналоговых модулей",
        },
        {
            id:"platformTableLayout_id",
            icon:"mdi mdi-table",
            value:"таблица платформ БМРЗ",
        },
        {
            id:"prjVersTableLayout_id",
            icon:"mdi mdi-table",
            value:"таблица версий БФПО",
        },
        {
            id:"prjRevTableLayout_id",
            icon:"mdi mdi-table",
            value:"таблица редакций БФПО"
        },
        {
            id:"armEditTableLayout_id",
            icon:"mdi mdi-table",
            value:"таблица версий ArmEdit"
        },
        {
            id:"authorTableLayout_id",
            icon:"mdi mdi-table",
            value:"таблица авторов БФПО"
        },
        {
            id:"communicationTableLayout_id",
            icon:"mdi mdi-table",
            value:"таблица протоколов инф. обмена"
        },
        {
            id:"relayAlgorithmTableLayout_id",
            icon:"mdi mdi-table",
            value:"таблица алгоритмов РЗиА"
        },
        {
            id:"projectTrees_id",
            icon:"mdi mdi-family-tree",
            value:"деревья изменений БФПО"
        },
        {
            id:"prohectHistory_id",
            icon:"mdi mdi-book-clock",
            value:"история изменений БФПО"
        }
    ];
    return result;
}

// запуск webix на выполнение:
webix.ready(function () {
    webix.ui(getMainLayout()).show();
});