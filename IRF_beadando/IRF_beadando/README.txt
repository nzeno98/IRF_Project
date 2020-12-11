IRF beadandó feladat- Nagy Zénó - QTRJBN

A szoftver egy a magyar koronavírus járvány terjedését grafikonon ábrázoló szoftver, mely egy -a felhasználó által megadott- egy hónapos intervallumot elemez. A grafikonok között nyilakkal lehet léptetni.

1. grafikon: Napi új esetek száma
2. grafikon: Napi halálesetek száma
3. grafikon: Teljes járványgörbe => ezesetben a dátum megadása nem elérhető
Az alkalmazás DataGridView segítségével számszerűsíti is az adatokat.

Elérhető egy mutató is, mely a járvány terjedésének gyorsaságát mutatja az aznapi új fertőzések fuggvényében, 0-5 skálán, ahol 0 a nagyon enyhe, míg 5 a kritikus mértékű terjedés.

Az alkalmazás az adatokat egy .csv fájlból nyeri, ez most a mellékelt Korona.csv.
A fájl 2020.03.05-től, tehát a betegség első igazolt magyarországi mefjelenésétől kezdve 2020.12.06-ig tartalmaz adatokat.
A fájl felépítése a következő: 
-fejléc nincs
-minden sorban egy dátum és 3 számadat található (kapcsos zárójel nélkül): {ÉÉÉÉ.HH.NN};{Napi fertőzöttek};{Napi halálos áldozatok};{Addigi összes fertőzött}
-a fájl a Korona.csv nevet és formátumot kell kapja, és az alkalmazás gyökérkönyvtárában szükséges elhelyezni.
A mellékelt Korona.csv fájl adatainak forrása: https://data.europa.eu/euodp/en/data/dataset/covid-19-coronavirus-data/resource/55e8f966-d5c8-438e-85bc-c7a5a26f4863