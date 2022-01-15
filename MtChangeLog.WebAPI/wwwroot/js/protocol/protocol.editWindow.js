class ProtocolEditWindow {
    constructor(editable){
        let _editable = editable;
        if(!_editable instanceof Protocol){
            throw new Error("не выбран протокол инф. обмена для работы");
        }

        let _uiWindow;
        this.show = function(){
            webix.ready(function (){
                _uiWindow = webix.ui(window());
                _uiWindow.show();
            });
        }

        // запуск прогрес бара при выполнении операций:
        let showProgress = function(){
            _uiWindow.disable();
            webix.extend(_uiWindow, webix.ProgressBar);
            _uiWindow.showProgress({ type:"icon" });
        }

        // остановка прогрес бара:
        let closeProgress = function(){
            _uiWindow.hideProgress();
            _uiWindow.enable();
        }

        let window = function (){
            let result = {
                view:"window",
                id:"win_id",
                modal:true,
                move:true,
                width:600,
                height:600,
		        position:"center",
                head:headLayout(),
                body:windowLayout()
            };
            return result;
        };
        
        let headLayout = function (){
            let result = {
                view:"toolbar",
                id:"headToolbar",
                height:40,
                elements:[
                    {
                        view:"label",
                        label:"Протокол инф. обмена:"
                    },
                    {
                        
                    },
                    {
                        view:"icon",
                        icon:"wxi-close",
                        id:"btn_oscWinClose",
                        align:"right",
                        tooltip:"закрыть окно",
                        click: function (){
                            _uiWindow.close();
                        }
                    }
                ]
            };
            return result;
        };

        let windowLayout = function () {
            let lWidth = 160;
            let result = {
                view:"layout",
                id:"winLayout",
                rows:[
                    { 
                        view:"text", 
                        label:"Наименование:", 
                        labelAlign:"right",
                        labelWidth:lWidth, 
                        value:_editable.getTitle(),
                        attributes:{
                            maxlength:32
                        }, 
                        on:{
                            onChange: async function(newValue, oldValue, config){
                                try{
                                    _editable.setTitle(newValue);
                                } catch (error){
                                    messageBox.warning(error.message);
                                }
                            }
                        }
                    },
                    {
                        view:"textarea",
                        label:"Описание:",
                        labelAlign:"right",
                        labelWidth:lWidth, 
                        height:120, 
                        value:_editable.getDescription(),
                        attributes:{
                            maxlength:500
                        }, 
                        on:{
                            onChange: function(newValue, oldValue, config){
                                try{
                                    _editable.setDescription(newValue);
                                } catch (error){
                                    messageBox.warning(error.message);
                                }
                            }
                        }
                    },
                    {
                        view:"multicombo", 
                        id:"protocolModules_id",
                        label:"Модули:",
                        labelAlign:"right",
                        labelWidth:lWidth,  
                        value:_editable.getCommunicationModules(),
                        button:true,
                        suggest:{
                            body:{
                                data:_editable.getAllCommunicationModules(),
                                template:webix.template("#title#")
                            }
                        }
                    },
                    {
                        view:"button",
                        id:"submitButton_id",
                        value:"Отправить",
                        css:"webix_primary",
                        inputWidth:200,
                        align:"right",
                        click: async function(){
                            try{ 
                                showProgress();
                                // обновить перечень платформ у модуля:
                                let selected = $$("protocolModules_id").getValue().split(",");
                                _editable.setCommunicationModules(selected);
                                
                                // отправить:
                                await _editable.submit();

                                // автоматически закрывать при удачном стечении обстоятельств:
                                _uiWindow.close();
                            } catch (error){
                                messageBox.alertWarning(error.message);
                            } finally{
                                closeProgress();
                            }
                        }
                    }
                ]
            };
            return result;
        };
    }
}