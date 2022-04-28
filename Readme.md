## This Project implement IEC104 master ( tcp client)  protocol

### `Goi lenh Dong/Mo IOA ( double and single command):`

* `Truong hop 1`: Doi voi IOA  yeu cau select before execute thi qua trinh gui cmd dong cat se nhu sau:
        
        1. Gui cmd select IOA (COT = activation)
        2. Gui cmd execute IOA (COT =activation)

* `Truong hop 2:` Doi voi IOA  khong yeu cau select before execute thi chi gui 1 cmd `execute` dong cat  la du, khong can cmd `select`

        1. Gui cmd execute IOA (COT =activation)

### `Note : Doi voi 1 so thiet bi, neu kiem tra truong hop 2(send 1 cmd exe)  ko dong cat duoc,  thi se kiem tra truong hop 1 ( send 2 cmd: 1 sel, 1 exe)`


* Doi voi BR_10R btr thi chi work voi `truong hop 2`:


        
* Doi voi NOJA vp thi chi work voi `truong hop 1`:

        * `S/E SingleCMD`