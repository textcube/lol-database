#!/bin/sh
BASEURL=http://d28xe8vt774jo5.cloudfront.net/abilities/videos/
for ((i=1;i<150;i++)); do
for ((j=1;j<6;j++)); do
URL=$BASEURL`printf "%04d_%02d.mp4" $i $j`
#echo $URL
/usr/bin/wget $URL
done
done
