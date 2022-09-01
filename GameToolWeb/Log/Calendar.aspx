<%@ page language="C#" autoeventwireup="true" inherits="Log_Calendar, App_Web_calendar.aspx.564c629f" %><!DOCTYPE html>
<html lang="ko">
<head>
<title></title>
<meta http-equiv="X-UA-Compatible" content="IE=9">
<meta charset="utf-8" />
<link rel="stylesheet" href="/Common/css/bootstrap.css" />
<link rel="stylesheet" href="/Common/css/customize.css" />
<style>
	body{padding:5px;}
</style>
<script src="/Common/js/jquery-1.8.3.js"></script>
<script src="/Common/js/bootstrap.min.js"></script>
</head>

<body>
<div class="miniWrap">
	<div class="calendar">
		<div class="header">
			<h5>2013년 1월</h5>
			<ul class="pager">
				<li class="previous"><a href="#">12월</a></li>
				<li class="next"><a href="#">2월</a></li>
			</ul>
		</div>
		<table class="table centerTable">
			<colgroup>
				<col style="width:15%" />
				<col style="width:14%" />
				<col style="width:14%" />
				<col style="width:14%" />
				<col style="width:14%" />
				<col style="width:14%" />
				<col style="width:15%" />
			</colgroup>
			<thead>
				<tr>
					<th>일</th>
					<th>월</th>
					<th>화</th>
					<th>수</th>
					<th>목</th>
					<th>금</th>
					<th>토</th>
				</tr>
			</thead>
			<tbody>
				<tr>
					<td><a href="#" class="grayText">30</a></td>
					<td><a href="#" class="grayText">31</a></td>
					<td><a href="#">1</a></td>
					<td><a href="#">2</a></td>
					<td><a href="#">3</a></td>
					<td><a href="#">4</a></td>
					<td><a href="#">5</a></td>
				</tr>
				<tr>
					<td><a href="#">6</a></td>
					<td><a href="#">7</a></td>
					<td><a href="#">8</a></td>
					<td><a href="#">9</a></td>
					<td><a href="#">10</a></td>
					<td><a href="#" class="btn btn-small btn-primary">11</a></td>
					<td><a href="#">12</a></td>
				</tr>
				<tr>
					<td><a href="#">13</a></td>
					<td><a href="#">14</a></td>
					<td><a href="#">15</a></td>
					<td><a href="#">16</a></td>
					<td><a href="#">17</a></td>
					<td><a href="#">18</a></td>
					<td><a href="#">19</a></td>
				</tr>
				<tr>
					<td><a href="#">20</a></td>
					<td><a href="#">21</a></td>
					<td><a href="#">22</a></td>
					<td><a href="#">23</a></td>
					<td><a href="#">24</a></td>
					<td><a href="#">25</a></td>
					<td><a href="#">26</a></td>
				</tr>
				<tr>
					<td><a href="#">27</a></td>
					<td><a href="#">28</a></td>
					<td><a href="#">29</a></td>
					<td><a href="#">30</a></td>
					<td><a href="#">31</a></td>
					<td><a href="#" class="grayText">1</a></td>
					<td><a href="#" class="grayText">2</a></td>
				</tr>
				<tr>
					<td><a href="#" class="grayText">3</a></td>
					<td><a href="#" class="grayText">4</a></td>
					<td><a href="#" class="grayText">5</a></td>
					<td><a href="#" class="grayText">6</a></td>
					<td><a href="#" class="grayText">7</a></td>
					<td><a href="#" class="grayText">8</a></td>
					<td><a href="#" class="grayText">9</a></td>
				</tr>
			</tbody>
		</table>
		<p>Choice Date : <input type="text" class="input-small" /> <input class="btn btn-primary" type="button" value="SET" /></p>
	</div>
</div>
</body>
</html>
