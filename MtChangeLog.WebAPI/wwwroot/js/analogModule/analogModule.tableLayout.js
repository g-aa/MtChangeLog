class AnalogModuleTableLayout {
  constructor(parentLayout) {
    this.parentLayout = parentLayout;

    let refresh = async function(){
      let url = entitiesRepository.getAnalogModulesUrl();
      let tableData = await entitiesRepository.getEntitiesInfo(url);
      let table = $$("analogModuleTable_id");
      table.parse(tableData);
      table.refresh();
    }

    this.tableLayout = {
      view: "datatable",
      id: "analogModuleTable_id", 
      select: "row",
      adjust: true,
      resizeColumn: true,
      columns: [
          { id: "id",             adjust: true,     header: ["GUID:", ""] },
          { id: "title",          adjust: true,     header: ["Наименование:", { content: "multiSelectFilter" }] },
          { id: "divg",           adjust: true,     header: ["ДИВГ:", ""] },
          { id: "current", adjust: true,     header: ["Номинальный ток:", { content: "multiSelectFilter" }] },
          { id: "description",    fillspace: true,  header: ["Описание:", ""] }
      ],
      data: []
    };

    let btnWidth = 200;
    this.buttonsLayout = {
      view: "toolbar", 
      cols: [
        { 
          view: "button", 
          css: "webix_primary",
          value: "Добавить", 
          width: btnWidth,
          click: async function () {
            try {
              let module = new AnalogModule();
              await module.defaultInitialize();
              module.beforeEnding = async function(url, answer){
                messageBox.information(answer);
                await refresh();
              };

              let win = new AnalogModuleEditWindow(module);
              win.show();
            } catch (error) {
              messageBox.error(error.message);
            }
          }
        },
        { 
          view: "button", 
          css: "webix_secondary",
          value: "Редактировать", 
          width: btnWidth,
          click: async function () {
            try {
              let selected = $$("analogModuleTable_id").getSelectedItem();
              if(selected != undefined) {
                let module = new AnalogModule();
                await module.initialize(selected);
                module.beforeEnding = async function(url, answer){
                  messageBox.information(answer);
                  await refresh();
                };

                let win = new AnalogModuleEditWindow(module);
                win.show();
              } else {
                messageBox.information("выберете аналоговый модуль для редактирования");
              }
            } catch (error) {
              messageBox.error(error.message);
            }
          }
        },
        { 
          view: "button",
          css: "webix_danger",
          value: "Удалить",
          width: btnWidth,
          align: "right",
          click: async function () {
            try {
              let module = $$("analogModuleTable_id").getSelectedItem();
              if(module != undefined) {
                let url = entitiesRepository.getAnalogModulesUrl();
                let answer = await entitiesRepository.deleteEntity(url, module);
                messageBox.information(answer);
                await refresh();
              } else {
                messageBox.information("выберете аналоговый модуль для удаления");
              }
            } catch (error) {
              messageBox.error(error.message); 
            }
          } 
        }
      ]
    }
  }

  show() {
    webix.ui({
        view: "layout",
        rows: [ this.buttonsLayout, this.tableLayout ]
      }, 
      this.parentLayout.getChildViews()[0]);

      let url = entitiesRepository.getAnalogModulesUrl();
      entitiesRepository.getEntitiesInfo(url)
      .then(tableData => {
        let table = $$("analogModuleTable_id");
        table.parse(tableData);
        table.refresh();
      })            
      .catch(error => {
        messageBox.error(error.message);
      });
  }
}