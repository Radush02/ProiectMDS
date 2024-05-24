// s3.service.ts

import { Injectable } from '@angular/core';
import S3 from 'aws-sdk/clients/s3';
import { environment } from '../../environments/environments'

@Injectable({
  providedIn: 'root',
})
export class S3Service {
  private s3: S3;

  constructor() {
    this.s3 = new S3({
      accessKeyId: environment.awsAccessKeyId,
      secretAccessKey: environment.awsSecretAccessKey,
      region: environment.awsRegion,
    });
  }

  getObjectUrl(bucket: string, key: string): string {
    return this.s3.getSignedUrl('getObject', {
      Bucket: bucket,
      Key: key,
    });
  }
  uploadFile(bucket: string, key: string, file: File): Promise<void> {
    const params = {
      Bucket: bucket,
      Key: key,
      Body: file,
    };

    return new Promise((resolve, reject) => {
      this.s3.upload(params, (err: any) => {
        if (err) {
          reject(err);
        } else {
          resolve();
        }
      });
    });
  }
  getFilesFromFolder(bucket: string, folder: string): Promise<string[]> {
    const params = {
      Bucket: bucket,
      Prefix: folder,
    };

    return new Promise((resolve, reject) => {
      this.s3.listObjectsV2(params, (err, data) => {
        if (err) {
          reject(err);
        } else {
          const files = data.Contents ? data.Contents.map((file) => file.Key).filter((key): key is string => Boolean(key)) : [];
          resolve(files);
        }
      });
    });
  }
}