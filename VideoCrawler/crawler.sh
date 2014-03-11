#!/usr/bin/php
<?php
$url_base = "http://d28xe8vt774jo5.cloudfront.net/abilities/videos/";
for ($i=1;$i<150;$i++) {
      for ($j=1;$j<6;$j++) {
            $url = $url_base . sprintf("%04d_%02d.mp4", $i, $j);
            //echo $url . "\n";
            exec ("wget " . $url);
      }
}
