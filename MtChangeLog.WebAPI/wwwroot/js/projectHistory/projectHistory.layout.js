class ProjectHistoryLayout{
    constructor(parentLayout){
        this.parentLayout = parentLayout;

        let viewId = "viewLayout_id";
        this.viewLayout = {
            view:"template",
            id:viewId,
            template: "<center>Зона отображения истории изменения БФПО...</center>"
        }

        let cbxId = "projectsCbx_id";
        this.cbxLayout = {
            view:"toolbar", 
            cols:[
                {
                    view:"richselect",
                    id:cbxId,
                    label:"Проект (БФПО):",
                    labelAlign:"right",
                    labelWidth:220,
                    icon:"mdi mdi-arrow-down",
                    value:"",
                    options:[],
                    on:{
                        onChange: async function(newValue, oldValue, config){
                            try {
                                let layout = $$(viewId);
                                let dataList = await entitiesRepository.getProjectHistory(newValue);
                                let newLayout = {
                                    view:"unitlist",
                                    id:viewId,
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
        this.cbxId = cbxId;
    }

    show(){
        webix.ui({
            view: "layout",
            rows: [ this.cbxLayout, this.viewLayout ]
        }, 
        this.parentLayout.getChildViews()[0]);
        
        let comboBox = $$(this.cbxId);
        entitiesRepository.getProjectsForHistorys()
        .then(projectsData => {
            comboBox.define("options", {
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