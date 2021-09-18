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
                                        case "projectTableLayout_id":
                                            if(projectLayout == undefined || projectLayout == null) {
                                                var projectLayout = new ProjectTableLayout(dLayout);
                                            }
                                            projectLayout.show();
                                            break;
                                        case "revisionTable_id":

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
            value:"таблица аналоговых модулей",
        },
        {
            id:"platformTableLayout_id",
            icon:"mdi mdi-table",
            value:"таблица платформ БМРЗ",
        },
        {
            id:"projectTableLayout_id",
            icon:"mdi mdi-table",
            value:"таблица версий БФПО",
        },
        {
            id:"revisionTable_id",
            icon:"mdi mdi-table",
            value:"таблица редакций БФПО"
        }
    ];
    return result;
}

// запуск webix на выполнение:
webix.ready(function () {
    webix.ui(getMainLayout()).show();
});