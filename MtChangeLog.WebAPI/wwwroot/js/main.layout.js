class MainLayout{
    constructor(){
        // основное меню:
        let mainMenuItems = [
            {
                id:"homeMenuItem_id",
                icon:"mdi mdi-home-circle",
                value:"начальная страница",
                getLayout:function(){
                    return new HomeLayout();
                }
            },
            {
                id:"lastPrjRevMenuItem_id",
                icon:"mdi mdi-file-check",
                value:"актуальные редакции БФПО",
                getLayout:function(){
                    return new LastProjectRevisionTableLayout();
                }
            },
            {
                id:"prjHistoryMenuItem_id",
                icon:"mdi mdi-format-list-text",
                value:"история изменений БФПО",
                getLayout:function(){
                    return new ProjectHistoryLayout();
                }
            },
            {
                id:"prjTreesMenuItem_id",
                icon:"mdi mdi-graph",
                value:"деревья изменений БФПО",
                getLayout:function(){
                    return new ProjectTreeLayout();
                }
            },
            {
                id:"authorMenuItem_id",
                icon:"mdi mdi-card-account-details",
                value:"таблица авторов БФПО",
                getLayout:function(){
                    return new AuthorTableLayout();
                }
            },
            {
                id:"algorithmMenuItem_id",
                icon:"mdi mdi-function-variant",
                value:"таблица алгоритмов РЗиА",
                getLayout:function(){
                    return new RelayAlgorithmTableLayout();
                }
            },
            {
                id:"armEditMenuItem_id",
                icon:"mdi mdi-application-brackets",
                value:"таблица версий ArmEdit",
                getLayout:function(){
                    return new ArmEditTableLayout();
                }
            },
            {
                id:"analogModuleMenuItem_id",
                icon:"mdi mdi-puzzle",
                value:"таблица аналоговых модулей",
                getLayout:function(){
                    return new AnalogModuleTableLayout();
                }
            },
            {
                id:"platformMenuItem_id",
                icon:"mdi mdi-application-cog-outline",
                value:"таблица платформ БМРЗ",
                getLayout:function(){
                    return new PlatformTableLayout();
                }
            },
            {
                id:"protocolMenuItem_id",
                icon:"mdi mdi-protocol",
                value:"таблица инф. протоколов",
                getLayout:function(){
                    return new ProtocolTableLayout();
                }
            },
            {
                id:"communicationMenuItem_id",
                icon:"mdi mdi-ethernet",
                value:"таблица коммуникаций",
                getLayout:function(){
                    return new CommunicationModuleTableLayout();
                }
            },
            {
                id:"prjStatusMenuItem_id",
                icon:"mdi mdi-list-status",
                value:"таблица статусов БФПО",
                getLayout:function(){
                    return new ProjectStatusTableLayout();
                }
            },
            {
                id:"prjVersMenuItem_id",
                icon:"mdi mdi-alpha-v-box",
                value:"таблица версий БФПО",
                getLayout:function(){
                    return new ProjectVersionTableLayout();
                }
            },
            {
                id:"prjRevMenuItem_id",
                icon:"mdi mdi-alpha-r-box",
                value:"таблица редакций БФПО",
                getLayout:function(){
                    return new ProjectRevisionLayout();
                }
            }
        ];
        
        // макет:
        let sidebarId = "mainSidebar_id";
        let contentLayoutId = "contentLayout_id";
        let mainLayout = {
            rows: [
                {
                    view:"toolbar",
                    id:"mainToolbar_id",
                    padding:3,
                    elements:[
                        { 
                            tooltip:"свернуть / развернуть основное меню",
                            view:"icon", 
                            icon:"mdi mdi-menu", 
                            click:function(){  
                                $$(sidebarId).toggle();
                            }
                        },
                        {
                            view:"label",
                            align:"center",
                            label:"<span style='font-size:30px;'>БМРЗ - change log</span>"
                        },
                        { 
                            view:"icon", 
                            icon:"mdi mdi-information", 
                            tooltip:"О программе",
                            click:function(){
                                // добавить вывод информации о программе
                            }
                        }
                    ]
                },
                {
                    cols:[
                        {
                            view:"sidebar",
                            width:280,
                            id:"mainSidebar_id",
                            data:mainMenuItems,
                            on:{
                                onItemClick:function(id){
                                    try {
                                        let contentLayout = $$(contentLayoutId);
                                        
                                        var result = mainMenuItems.find(function(item){
                                            return item.id == id;
                                        });
                                        
                                        if(result == undefined){
                                            messageBox.warning("Увы, функционал пока не поддерживается!");
                                            return;
                                        }
                                        result.getLayout().show(contentLayout);
                                    } catch(error){
                                        messageBox.error(error.message);
                                    }
                                }
                            }
                        },
                        {
                            view:"layout",
                            id:contentLayoutId,
                            rows:[
                                {
                                    view:"template",
                                    template:"Default template with some text inside"
                                }
                            ]
                        }
                    ]
                }
            ]
        };

        // запуск webix на выполнение:
        this.show = function(){
            webix.ready(function () {
                webix.ui(mainLayout).show();
                $$(sidebarId).select(mainMenuItems[0].id);   
                $$(sidebarId).callEvent("onItemClick", [ mainMenuItems[0].id ]);
            });
        }
    }
}

var mainLayout = new MainLayout();
mainLayout.show();