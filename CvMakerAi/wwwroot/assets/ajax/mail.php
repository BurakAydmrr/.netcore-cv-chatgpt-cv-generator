<?php
parse_str($_POST['form_data'], $form);

define('TO_EMAIL', 'themewar@gmail.com');
define('SUBJECT', 'Shapro User Query');
define('FROM_EMAIL', $form['con_email']);

$MESSAGE = 'Hi Admin, <br/><br/>';
$MESSAGE .= 'You got an user query from Shapro. User details and Message are noted bellow: <br/><br/>';
$MESSAGE .= 'Name : '.$form['con_name'].'<br/>';
$MESSAGE .= 'Email : '.$form['con_email'].'<br/>';
$MESSAGE .= 'Website : '.$form['con_website'].'<br/>';
$MESSAGE .= 'Message : <br/>'.$form['con_message'].'<br/><br/>';
$MESSAGE .= 'Regards';

$HEADERS = "MIME-Version: 1.0" . "\r\n";
$HEADERS .= "Content-type:text/html;charset=UTF-8" . "\r\n";
$HEADERS .= 'From: <'.FROM_EMAIL.'>' . "\r\n";

mail(TO_EMAIL, SUBJECT, $MESSAGE, $HEADERS);
echo 1;
exit();