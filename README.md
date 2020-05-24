# PDFConverter
PDFConverter API with sautinsoft document.net


Install dotenetcore 3.1
https://docs.microsoft.com/en-us/dotnet/core/install/linux-package-manager-ubuntu-1804

Install GDip ( for Drawing dependency) : 
`` sudo apt install libgdiplus``

install the fonts inside the fonts folder
``` 
sudo cp -r fonts /usr/share/fonts/truetype/pleco
cd /usr/share/fonts/truetype
sudo apt install xfonts-utils
sudo mkfontscale && sudo mkfontdir
sudo fc-cache -f -v
```

To check if font is installed
``fc-list | grep -i "Damindu"``

run with 
``dotnet run``

update the yml of pleco to the correct url (eg : https://localhost:5000/api/converter

### To automate startup 
First build the project by,
```
cd PDFConverter
dotnet publish --configuration Release
```
Source : https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/linux-apache?view=aspnetcore-3.1
You may need to update exec path

place the script at /etc/systemd/system/pdfconv.service
```
[Unit]
Description=PDF Converter for pleco

[Service]
Type=notify
ExecStart=/usr/bin/dotnet /home/plecoadm/PDFConverterRepo/PDFConverter/bin/Release/netcoreapp3.1/PDFConverter.dll
User=plecoadm
Restart=always
# Restart service after 10 seconds if the dotnet service crashes:
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=pdfcon
Environment=ASPNETCORE_ENVIRONMENT=Production

[Install]
WantedBy=multi-user.target
```

### To start the service
```
sudo systemctl daemon-reload
```
### Enable at start up of server
```
sudo systemctl enable pdfconv
sudo systemctl start pdfconv
```
### Can check if it has successfully started 

sudo journalctl -f -n 1000 -u pdfconv

### Test the deployment by
```
wget http://localhost:5000/api/converter
``` 
This will create a file named converter with response.
To test a file conversion
```
wget  --post-data 'pdfFilePath=/home/plecoadm/PDFConverterRepo/testPDF/I - IIA (T) 2009.08.28.pdf&docxFilePath=/home/plecoadm/PDFConverterRepo/testPDF/I - IIA (T) 2009.08.28.docx' https://localhost:5001/api/converter --no-check-certificate

wget  --post-data 'pdfFilePath=/home/plecoadm/PDFConverterRepo/testPDF/04-2010(s).pdf&docxFilePath=/home/plecoadm/PDFConverterRepo/testPDF/04-2010(s).docx' https://localhost:5001/api/converter --no-check-certificate
``` 
This will create a response file named converter, cat the file to see if it has string success.
If success check if the converted file is placed under specified docx file path: ``/home/plecoadm/PDFConverterRepo/testPDF/I - IIA (T) 2009.08.28.docx``
