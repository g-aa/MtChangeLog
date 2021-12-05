class LogWindow{
    constructor(entityId){
        if(entityId == undefined || typeof(entityId) != "string"){
            throw new Error("не передан id сущности");
        }
        
        let uiWindow;
        let logLayoutId = "logLayout_id";
        this.show = async function(){
            let logData = await repository.getProjectRevisionHistory(entityId);
            webix.ready(function(){    
                uiWindow = webix.ui(window());
                uiWindow.show();
            });
            let logLayout = $$(logLayoutId);
            logLayout.setValues(logData, true);
        };

        let window = function () {
            let result = {
                view:"window",
                id:"win_id",
                modal:true,
                move:true,
                resize:true,
                width:900,
                height:600,
		        position:"center",
                head:headLayout(),
                body:windowLayout()
            };
            return result;
        };

        let headLayout = function () {
            let result = {
                view:"toolbar",
                id:"headToolbar",
                height:40,
                elements:[
                    {
                        view:"label",
                        label:"Запись об изменениях:"
                    },
                    {
                        
                    },
                    {
                        view:"icon",
                        icon:"wxi-close",
                        id:"btn_oscWinClose",
                        align:"right",
                        tooltip:"закрыть окно",
                        click: function(){
                            uiWindow.close();
                        }
                    }
                ]
            };
            return result;
        };

        let windowLayout = function(){
            let result = {
                view:"template",
                id:logLayoutId,
                scroll:true,
                template: function(o){
                    let res = "<b>Дата:</b><span style='white-space: pre-wrap'>\t\t\t" + o.date + "</span><br>"
                        + "<br><b>ArmEdit:</b><span style='white-space: pre-wrap'>\t\t" + o.armEdit + "</span><br>"
                        + "<br><b>Платформа:</b><span style='white-space: pre-wrap'>\t" + o.platform + "</span><br>"
                        + "<br><b>Протоколы:</b><span style='white-space: pre-wrap'>\t\t" + o.communication + "</span><br>"
                        + "<br><b>Авторы:</b><span style='white-space: pre-wrap'>\t\t" + (o.authors != undefined ? o.authors.join(", ") : "") + "</span><br>"
                        + "<br><b>Причины:</b><span style='white-space: pre-wrap'>\t\t" + o.reason + "</span><br>"
                        + "<br><b>Описание:</b><br><span style='white-space: pre-wrap'>" + o.description + "</span>";
                    return res;
                }
            };
            return result;
        };
    }
}