import { FormGroup } from "@angular/forms";

export function mapFormToModel<ModelType>(form : FormGroup) : ModelType{
    const model : any = {};
    Object.keys(form.controls)
        .forEach(key => {
            model[key] = form.get(key)?.value;
        });
    return model as ModelType;
}

export function mapModelToForm(model : any, form : FormGroup) : FormGroup{
    Object.keys(form.controls).forEach(key => {
        if(model.hasOwnProperty(key))
            form.controls[key].setValue((model as any)[key]);
    })

    return form;
}