class StartPageLayout{
    constructor(parentLayout){
        this.parentLayout = parentLayout;

        let statisticsLayoutId = "statisticsLayout_id";
        let mostChangedLayoutId = "mostChangedLayout_id";
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
                                    template:"10 часто редактируемых БФПО:" 
                                }, 
                                {
                                    view:"list",
                                    id:mostChangedLayoutId,    
                                    select:true,
                                    template:function(o){
                                        let result = "<b>Дата:</b><span style='white-space: pre-wrap'> " + o.date + "\t</span>"
                                            + "<b>БФПО:</b><span style='white-space: pre-wrap'> " + o.title + "</span>"; 
                                        return result;
                                    },
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
                                let win = new LogWindow(id.row);
                                win.show();
                            } catch (error){
                                messageBox.alertWarning(error.message);
                            }
                        }
                    },
                    columns:[
                        { id:"date",        width:250,  header:["Дата релиза:"] },
                        { id:"title",       width:350,  header:["10 недавно измененных БФПО:"] },
                        { id:"platform",    width:150,  header:["Платформа:"] },
                        { fillspace:true }
                    ],
                    data:[]
                }
            ]
        };
        this.statisticsLayoutId = statisticsLayoutId;
        this.mostChangedLayoutId = mostChangedLayoutId;
        this.lastChangedLayoutId = lastChangedLayoutId;
    }
    show(){
        webix.ui(this.viewlayout, this.parentLayout.getChildViews()[0]);

        let statlayout = $$(this.statisticsLayoutId);
        let mostLayout = $$(this.mostChangedLayoutId);
        let lastLayout = $$(this.lastChangedLayoutId);
        repository.getShortStatistics()
        .then(data => {
            statlayout.setValues(data, true);
            mostLayout.define("data", data.mostChangingProjects);
            lastLayout.parse(data.lastModifiedProjects);
            lastLayout.refresh();
        })
        .catch(error => {
            messageBox.error(error.message);
        });
    }
}