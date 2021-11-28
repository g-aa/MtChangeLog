class StartPageLayout{
    constructor(parentLayout){
        this.parentLayout = parentLayout;

        let layoutId = "startLayout_id";
        this.viewLayout = { 
            view:"template",
            id:layoutId,
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
        this.layoutId = layoutId;
    }
    show(){
        webix.ui({
            view: "layout",
            rows: [ { view:"template", type:"header", template:"Общая статистика"}, this.viewLayout ]
        }, 
        this.parentLayout.getChildViews()[0]);
        let layout = $$(this.layoutId);
        repository.getShortStatistics()
        .then(data => {
                layout.setValues(data, true);
        })
        .catch(error => {
            messageBox.error(error.message);
        });
    }
}