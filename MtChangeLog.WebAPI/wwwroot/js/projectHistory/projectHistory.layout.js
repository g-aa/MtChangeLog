class ProjectHistoryLayout{
    constructor(){
        let viewLayoutId = "viewLayout_id";
        this.viewLayout = {
            view:"template",
            id:viewLayoutId,
            template: "<center>Зона отображения истории изменения БФПО...</center>"
        }

        let cbxLayoutId = "projectsCbx_id";
        this.cbxLayout = {
            view:"toolbar", 
            cols:[
                {
                    view:"richselect",
                    id:cbxLayoutId,
                    label:"Проект (БФПО):",
                    labelAlign:"right",
                    labelWidth:220,
                    icon:"mdi mdi-alpha-v-box",
                    value:"",
                    options:[],
                    on:{
                        onChange: async function(newValue, oldValue, config){
                            try {
                                let layout = $$(viewLayoutId);
                                let dataList = await repository.getProjectVersionHistory(newValue);
                                let newLayout = {
                                    view:"unitlist",
                                    id:viewLayoutId,
                                    uniteBy: function(o){
                                        return o.title;
                                    },
                                    template: function(o){
                                        let res = "<b>Дата:</b><span style='white-space: pre-wrap'>\t\t\t" + o.date + "</span><br>"
                                            + "<b>ArmEdit:</b><span style='white-space: pre-wrap'>\t\t" + o.armEdit + "</span><br>"
                                            + "<b>Платформа:</b><span style='white-space: pre-wrap'>\t" + o.platform + "</span><br>"
                                            + "<b>Протоколы:</b><span style='white-space: pre-wrap'>\t\t" + o.communication + "</span><br>"
                                            + "<b>Авторы:</b><span style='white-space: pre-wrap'>\t\t" + (o.authors != undefined ? o.authors.join(", ") : "") + "</span><br>"
                                            + "<b>Причины:</b><span style='white-space: pre-wrap'>\t\t" + o.reason + "</span><br>"
                                            + "<b>Описание:</b><br><span style='white-space: pre-wrap'>" + o.description + "</span>";
                                        return res; 
                                    },
                                    type:{
                                        height:"auto"
                                    },
                                    select:true,
                                    data:dataList
                                }
                                webix.ui(newLayout, layout);
                            } catch (error){
                                messageBox.alertWarning(error.message);
                            }
                        }
                    }
                },
                {

                }
            ]
        }
        this.cbxLayoutId = cbxLayoutId;
    }

    show(parentLayout){
        webix.ui({
            view: "layout",
            rows: [ this.cbxLayout, this.viewLayout ]
        }, 
        parentLayout.getChildViews()[0]);
        let cbxLayout = $$(this.cbxLayoutId);
        repository.getProjectHistoryTitles()
        .then(projectsData => {
            cbxLayout.define("options", {
                body:{
                    template:"#module#-#title#-#version#"
                },   
                data:projectsData
            });
        })
        .catch(error => {
            messageBox.error(error.message);
        });
    }
}