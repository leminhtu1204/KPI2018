import { Component, Inject, ViewChild } from '@angular/core';
import { HttpClient, HttpRequest, HttpEventType, HttpResponse } from '@angular/common/http'
@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
})
export class MenuComponent {
  private base = 'http://comchui.azurewebsites.net/api/';

  public progress: number;
  public message: string;
  @ViewChild("file") fileInput;
  
  constructor(private http: HttpClient) {
   
  }

  public upload() {
    if (!this.fileInput)
      return;
     let fi = this.fileInput.nativeElement;
     if (fi.files && fi.files[0]) {
      let fileToUpload = fi.files[0];
      let formData = new FormData();
      formData.append("file", fileToUpload);

      const uploadReq = new HttpRequest('POST', this.base + "menus/uploading", formData, {
        reportProgress: true,
      });
      
       this.http.request(uploadReq).subscribe(event => {
        if(event.type === HttpEventType.UploadProgress)
          this.progress = Math.round(100 * event.loaded / event.total);
          if(this.progress === 100){
            this.message = "upload successfully";
          }
      });
    }
  }
}