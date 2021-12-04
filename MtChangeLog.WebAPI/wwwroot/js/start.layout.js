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
                                    template:function(o){
                                        let result = "<b>Дата:</b><span style='white-space: pre-wrap'>\t\t\t\t\t\t" + o.date + "</span><br><br>"
                                        + "<b>Актуальная версия ArmEdit:</b><span style='white-space: pre-wrap'>\t" + o.armEdit + "</span><br><br>"
                                        + "<b>Число БФПО (проектов):</b><span style='white-space: pre-wrap'>\t" + o.projectCount + "</span><br><br>"
                                        + "<b>Число актуальных БФПО:</b><span style='white-space: pre-wrap'>\t" + o.actualProjectCount + "</span><br><br>"
                                        + "<b>Число тестовых БФПО:</b><span style='white-space: pre-wrap'>\t\t" + o.testProjectCount + "</span><br><br>"
                                        + "<b>Число устаревших БФПО:</b><span style='white-space: pre-wrap'>\t" + o.deprecatedProjectCount + "</span><br><br>";
                                        return result;
                                    },
                                    data:{
                                        date: new Date().toLocaleString(),
                                        armEdit:"нет данных...",
                                        projectCount:"нет данных...",
                                        actualProjectCount:"нет данных...",
                                        testProjectCount:"нет данных...",
                                        deprecatedProjectCount:"нет данных..."
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