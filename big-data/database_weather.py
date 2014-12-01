from collections import defaultdict
import matplotlib.pyplot as plt
import numpy as np

tmaxfile = "daily_maxt.txt"#"dly-tmax-normal.txt"
tmpmaxfile= open(tmaxfile,'r')
cont_tmax = tmpmaxfile.read().splitlines()
tminfile = "daily_mint.txt"#"dly-tmin-normal.txt"
tmpminfile= open(tminfile,'r')
cont_tmin = tmpminfile.read().splitlines()

stationsfile="temp-inventory.txt"
stfile= open(stationsfile,'r')
cont_stfile = stfile.read().splitlines()
stationlist = defaultdict(dict)
for s in cont_stfile:    
    longlat=[s.split()[1],s.split()[2]]
    stationlist[s.split()[0]]=longlat
from pymongo import MongoClient
client =  MongoClient('localhost', 27017)
#sudo service mongodb restart
db = client.mydb
db = client["mydb"]
weather = db.weather
weather = db["weather"]
weather.remove({})
cntline=0
cntday=0
statbef=""
for l in cont_tmax:
    stat = l.split()[0]
    month = l.split()[1]
    cntday=0
    if(stat!=statbef):
       statbef=stat
    linetmax = l[20:]
    linetmin = cont_tmin[cntline][20:]
    for i in range(0,len(linetmax.split("   ")) ):
        cntday+=1
        if( linetmax.split()[i][0:1]=='-'):
           tmax = linetmax.split()[i][0:3]+'.'+linetmax.split()[i][3:len(linetmax.split()[i])-1]
        else:
           if(len(linetmax.split()[i])>2):
              tmax = linetmax.split()[i][0:2]+'.'+linetmax.split()[i][2:3]
           else:
              tmax = linetmax.split()[i][0:1]
        if( linetmin.split()[i][0:1]=='-'):
           tmin = linetmin.split()[i][0:len(linetmin.split()[i])-1]
        else:
           if(len(linetmin.split()[i])>2):
              tmin = linetmin.split()[i][0:2]+'.'+linetmin.split()[i][2:len(linetmin.split()[i])-1]
           elif(len(linetmin.split()[i])==2):
              tmin = linetmin.split()[i][0:1]
           else:
              tmin = linetmin.split()[i][0:1]
        date = str(cntday)+'-'+str(month)
        weather.insert({"latitude": stationlist[stat][0],"longitude": stationlist[stat][1], "date": date,"Tmax": tmax,"Tmin": tmin})
    cntline+=1

cnt=0
temp_avmax = []
temp_avmin = []
temp_av_tav = []
for e in weather.find({"latitude": stationlist['USC00210018'][0],"longitude": stationlist['USC00210018'][1]}):
    while( cnt< 91):
       cnt +=1
       print cnt,' ',e
       temp_avmax.append(float(e['Tmax']))
       temp_avmin.append(float(e['Tmin']))
       temp_av_tav.append( (float(e['Tmax'])+float(e['Tmin']))/2.0 )

lastyearweatherfile = 'USC00210018_2013_temp.txt'
stat='USC00210018'
lstyw = open(lastyearweatherfile,'r')
cont_wlsty = lstyw.read().splitlines()
db1 = client.mydb1
db1 = client["mydb1"]
wlsty = db1.weather
wlsty = db1["wlsty"]
wlsty.remove({})

templsty_tmax = []
templsty_tmin = []
templsty_tav = []
for l in cont_wlsty:
    date=str(l.split()[2])+'-'+str(l.split()[1])
    tmax =l.split()[3]
    tmin =l.split()[4]
    templsty_tmax.append(float(tmax))
    templsty_tmin.append(float(tmin))
    templsty_tav.append((float(tmin)+float(tmax))/2.0)
    
    wlsty.insert({"latitude": stationlist[stat][0],"longitude": stationlist[stat][1], "date": date,"Tmax": tmax,"Tmin": tmin})


x= np.arange(1,len(temp_avmax)+1,1.0)
plt.figure(1)
plt.plot(x,temp_av_tav,'ro',label='average 1980-2010')
x= np.arange(1,len(templsty_tav)+1,1.0)
plt.plot(x,templsty_tav,'bs',label='2013')
plt.legend(loc=2)
plt.xlabel('January-March (first quarter)')
plt.ylabel('(Tmax-Tmin)/2')
plt.title('(Tmax-Tmin)/2 at the station USC00210018 (latitude=47.2992,longitude=-965161')
plt.savefig('tmeancomp.png')
