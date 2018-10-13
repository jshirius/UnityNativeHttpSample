<?php
	//echo $_POST["unity_data"];
	
	//ファイルデータ
	$file = 'unity_data.txt';

	// ファイルをオープンして既存のコンテンツを取得します
	$current = file_get_contents($file);
	$current = $_POST["unity_data"];

	// 結果をファイルに書き出します
	file_put_contents($file, $current);
?>