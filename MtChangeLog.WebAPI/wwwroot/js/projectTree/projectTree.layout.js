class ProjectTreeLayout{
    constructor(parentLayout){
        this.parentLayout = parentLayout;

        let treeId = "treeLayout_id";
        this.treeLayout = {
            view: "template",
            id:treeId,
            template: "<center>Зона отображения деревьев изменения (редакций) БФПО...</center>"
        }

        /*
        this.treeLayout = {
            view:"diagram",
            id:treeId,
            autoplace: false,
            select:true,
            data:[],
            links:[],
            on:{
                onItemClick: async function(id){							
                    try{
                        //let res = getRevisionDetails(id);
                        //let log = new logWindow(res);
                        //log.show();
                    } catch(ex){
                        webix.message(ex.message);
                    }
                }
            }
        };
        */
        
        let projectTypeId = "projectType_id";
        this.buttonsLayout = {
            view:"toolbar", 
            cols:[
                {
                    view:"richselect",
                    id:projectTypeId,
                    label:"Тип проекта БМРЗ (тип БФПО):",
                    labelAlign:"right",
                    labelWidth:220,
                    icon:"mdi mdi-arrow-down",
                    value:"",
                    options:[],
                    on:{
                        onChange: async function(newValue, oldValue, config){
                            try {
                                let treeDiagram = $$(treeId);
                                let data = await entitiesRepository.getProjectTree(newValue);

                                if(data == undefined || data == null){
                                    return;
                                }

                                let treeData = getDataAndLinksForTree(data);

                                

                                // очистка данных:
                                //treeDiagram.clearAll();
                                //treeDiagram.getLinks().clearAll();
                                
                                // обновление данных:
                                //treeDiagram.parse(treeData.data);
                                //treeDiagram.getLinks().parse(treeData.links);

                                // убрать выбор элемента:
                                //treeDiagram.unselectAll();

                                
                                let newLayout = {
                                    view:"diagram",
                                    id:treeId,
                                    autoplace: false,
                                    select:true,
                                    data:treeData.data,
                                    links:treeData.links,
                                    on:{
                                        onItemClick: function(id){							
                                            try{
                                                //let res = getRevisionDetails(id);
                                                //let log = new logWindow(res);
                                                //log.show();
                                            } catch(ex){
                                                webix.message(ex.message);
                                            }
                                        }
                                    }
                                };
                                webix.ui(newLayout, treeDiagram); 
                                
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
        this.projectTypeId = projectTypeId;
    }

    show(){
        webix.ui({
            view: "layout",
            rows: [ this.buttonsLayout, this.treeLayout ]
        }, 
        this.parentLayout.getChildViews()[0]);
        
        let treeLayout = $$(this.projectTypeId);
        entitiesRepository.getProjectTypes()
        .then(data => {
            treeLayout.define("options", data);
        })
        .catch(error => {
            messageBox.error(error.message);
        });
    }
}