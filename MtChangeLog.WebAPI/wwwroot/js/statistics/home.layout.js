class HomeLayout{
    constructor(){
        let statisticsLayoutId = "statisticsLayout_id";
        let authorContributionLayoutId = "authorContributionLayout_id";
        let lastChangedLayoutId = "lastChangedLayout_id";
        this.viewlayout = {
            view:"layout",
            rows:[
                {
                    view:"layout",
                    cols:[
                        { 
                            view:"layout", 
                            rows:[ 
                                { 
                                    view:"template", 
                                    type:"header", 
                                    template:"Общая статистика:" 
                                }, 
                                {
                                    view:"template",
                                    id:statisticsLayoutId,
                                    scroll:true,
                                    template:function(o){
                                        let result = "<table cellspacing='8'><tbody>" 
                                        + "<tr><td><b>Данные на момент времени:</b></td><td>" + o.date + "</td></tr>"
                                        + "<tr><td><b>Актуальная версия ArmEdit:</b></td><td>" + o.armEdit + "</td></tr>"
                                        + "<tr><td><b>Число БФПО (проектов):</b></td><td>" + o.projectCount + "</td></tr>"
                                        + "<tr><td><b>Распределение БФПО по статусам:</b></td></tr>";
                                        for (var key in o.projectDistributions) {
                                            result += "<tr><td><b>• " + key + ":</b></td><td>" + o.projectDistributions[key] + "</td><tr>";
                                        }
                                        result += "</tbody></table>";
                                        return result;
                                    },
                                    data:{
                                        date: new Date().toLocaleString(),
                                        armEdit:"нет данных...",
                                        projectCount:"нет данных...",
                                        actualProjectCount:"нет данных...",
                                        testProjectCount:"нет данных...",
                                        deprecatedProjectCount:"нет данных...",
                                        projectDistributions:{"нет данных":"..."}
                                    }
                                }
                            ] 
                        },
                        { 
                            view:"layout", 
                            rows:[ 
                                { 
                                    view:"template", 
                                    type:"header", 
                                    template:"Вклады авторов в БФПО (краткий обзор):" 
                                }, 
                                {
                                    view:"datatable",
                                    id:authorContributionLayoutId, 
                                    adjust:true,
                                    resizeColumn:true,
                                    scroll:"xy",
                                    columns:[
                                        { id:"author",          width:350,  header:["Автор:"] },
                                        { id:"contribution",    width:150,  header:["Вклад:"] },
                                        { fillspace:true }
                                    ],
                                    data:[]
                                }  
                            ] 
                        }
                    ]
                },
                {
                    view:"datatable",
                    id:lastChangedLayoutId, 
                    select:"row",
                    adjust:true,
                    resizeColumn:true,
                    scroll:"xy",
                    on:{
                        onItemClick: async function(id){
                            try {
                                mainLayout.showProgress();
                                let win = new LogWindow(id.row);
                                await win.show();
                            } catch (error){
                                messageBox.alertWarning(error.message);
                            } finally {
                                mainLayout.closeProgress();
                            }
                        }
                    },
                    columns:[
                        { id:"date",        width:150, template:function(obj){ 
                                let format = webix.Date.dateToStr("%Y-%m-%d");
                                return format(new Date(obj.date)); 
                            }, header:["Дата релиза:"] },
                        { id:"title",       width:350,  header:["10 недавно измененных БФПО:"] },
                        { id:"platform",    width:150,  header:["Платформа:"] },
                        { fillspace:true }
                    ],
                    data:[]
                }
            ]
        };
        this.statisticsLayoutId = statisticsLayoutId;
        this.authorContributionLayoutId = authorContributionLayoutId;
        this.lastChangedLayoutId = lastChangedLayoutId;
    }
    async show(parentLayout){
        webix.ui(this.viewlayout, parentLayout.getChildViews()[0]);
        let data = await repository.getShortStatistics();
        
        let statlayout = $$(this.statisticsLayoutId);
        statlayout.setValues(data, true);

        let lastLayout = $$(this.lastChangedLayoutId);
        lastLayout.parse(data.lastModifiedProjects);

        let contributionLayout = $$(this.authorContributionLayoutId);
        contributionLayout.parse(data.authorContributions);
        lastLayout.refresh();
    }
}