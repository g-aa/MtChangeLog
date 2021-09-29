let getMainLayout = function () {
    let result = {
        rows: [
            {
                view: "toolbar",
                id: "mainToolbar_id",
                padding: 3,
                elements: [
                    { 
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
                        id: "mainSidebar_id",
                        data: getMainSidebarMenu(),
                        on: {
                            onItemClick: function (id) {
                                try {
                                    webix.message({
                                        text: "Был выбран: \'" + this.getItem(id).value + "\'",
                                        type: "info", 
                                        expire: 10000,
                                    });

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
                                        default:
                                            webix.message({
                                                text: "Увы, функционал пока не поддерживается!",
                                                type: "debug",
                                                expire: 10000,
                                            });
                                    }
                                }
                                catch(ex) {
                                    webix.message({
                                        text: ex.message,
                                        type: "error", 
                                        expire: 10000,
                                    });
                                }
                                finally {

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
            value:"аналоговые модули",
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
            value:"авторы проектов"
        },
        {
            id:"communicationTableLayout_id",
            icon:"mdi mdi-table",
            value:"протоколы инф. обмена"
        },
        {
            id:"relayAlgorithmTableLayout_id",
            icon:"mdi mdi-table",
            value:"алгоритмы РЗиА"
        }
    ];
    return result;
}

// запуск webix на выполнение:
webix.ready(function () {
    webix.ui(getMainLayout()).show();
});