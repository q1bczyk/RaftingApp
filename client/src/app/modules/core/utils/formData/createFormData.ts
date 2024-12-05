import { FormGroup } from "@angular/forms";

export function createFormData(form: FormGroup): FormData {
    const formData = new FormData(); 

    Object.keys(form.controls).forEach((key) => {
        const value = form.get(key)?.value;
        if (value !== undefined && value !== null) {
            if (key === 'file' && value instanceof File) formData.append(key, value, value.name); 
            else formData.append(key, value); 
        }
    });
    
    return formData;
}