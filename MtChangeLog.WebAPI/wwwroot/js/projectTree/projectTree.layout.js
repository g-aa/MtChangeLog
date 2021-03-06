class ProjectTreeLayout{
    constructor(){
        let viewLayoutId = "treeLayout_id";
        this.viewLayout = {
            view:"template",
            id:viewLayoutId,
            template:"<center>Зона отображения деревьев изменения (редакций) БФПО...</center>"
        }

        let cbxLayoutId = "projectType_id";
        this.cbxLayout = {
            view:"toolbar", 
            padding:3,
            elements:[
                {
                    view:"richselect",
                    id:cbxLayoutId,
                    label:"Тип проекта БМРЗ (тип БФПО):",
                    labelAlign:"right",
                    labelWidth:220,
                    icon:"mdi mdi-order-bool-ascending-variant",
                    value:"",
                    options:[],
                    on:{
                        onChange: async function(newValue, oldValue, config){
                            try {
                                mainLayout.showProgress();
                                let treeDiagram = $$(viewLayoutId);
                                let data = await repository.getProjectTree(newValue);
                                if(data != undefined){
                                    // преобразовать данные для webix diagram:
                                    let treeData = getDataAndLinksForTree(data);
                                    if(treeDiagram.name != "diagram"){
                                        let newLayout = {
                                            view:"diagram",
                                            id:viewLayoutId,
                                            autoplace:false,
                                            select:true,
                                            data:treeData.data,
                                            links:treeData.links,
                                            on:{
                                                onItemClick: async function(id){							
                                                    try {
                                                        mainLayout.showProgress();
                                                        let win = new LogWindow(id);
                                                        await win.show();
                                                    } catch (error){
                                                        messageBox.alertWarning(error.message);
                                                    } finally{
                                                        mainLayout.closeProgress();
                                                    }
                                                }
                                            }
                                        };
                                        webix.ui(newLayout, treeDiagram);
                                    } else{
                                        // очистка данных:
                                        treeDiagram.clearAll();
                                        treeDiagram.getLinks().clearAll();
                                        // обновление данных:
                                        treeDiagram.parse(treeData.data);
                                        treeDiagram.getLinks().parse(treeData.links);
                                        // убрать выбор элемента:
                                        treeDiagram.unselectAll();
                                    }
                                }
                            } catch (error){
                                messageBox.alertWarning(error.message);
                            } finally {
                                mainLayout.closeProgress();
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

    async show(parentLayout){
        webix.ui({
            view: "layout",
            rows: [ this.cbxLayout, this.viewLayout ]
        }, 
        parentLayout.getChildViews()[0]);

        let titles = await repository.getProjectTreeTitle();
        let cbxLayout = $$(this.cbxLayoutId);
        cbxLayout.define("options", titles);
    }
}