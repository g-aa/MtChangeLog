class InfoWindow{
    constructor(){
        let uiWindow;
        this.show = async function(){
            webix.ready(function(){    
                uiWindow = webix.ui(window());
                uiWindow.show();
            });
        };

        let window = function () {
            let result = {
                view:"window",
                id:"win_id",
                modal:true,
                move:false,
                resize:false,
                width:700,
                height:440,
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
                        label:"Информация о проекте:"
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
                view:"layout",
                type:"clean",
                rows:[
                    {
                        view:"template",
                        autoheight:true,
                        template:"<div align='center'><h2>MTChangeLog - v0.0.0</h2></div>"
                    },
                    { 
                        view:"layout",
                        type:"clean",
                        cols:[
                            {
                                view:"template",
                                autoheight:true,
                                width:270,
                                template:"<div align='center'><img src='logFile_x512.png' width='256' height='256' alt='lorem'></div>"
                            },
                            {
                                view:"template",
                                template:"<div align='center'><span><b>Приложение предназначено для "
                                            + "<br>отслеживания и регистрации изменений, "
                                            + "<br>в программном обеспечении устройств автоматизации "
                                            + "<br>(БМРЗ-100/120/150/160/M4) "
                                            + "<br>электроэнергетической системы (ЭЭС) "
                                            + "<br>НТЦ Механотроники.</b></div>"
                            }
                        ]
                    },
                    {
                        view:"template",
                        autoheight:true,
                        template:"<div align='center'><a href='https://github.com/g-aa/MtChangeLog'>github.com/g-aa/MtChangeLog</a><br><br><span>2021 - 2022</span></div>"
                    },
                    {
                        view:"template",
                        autoheight:true,
                        template:""
                    }
                ]
            };
            return result;
        };
    }
}