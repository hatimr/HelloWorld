using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace HelloWorld
{
	[Activity (Label = "HelloWorld", MainLauncher = true)]
	public class MainActivity : Activity
	{
		//int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			//to set the layout as vertical
			var layout = new LinearLayout (this);
			layout.Orientation = Orientation.Vertical;

			int currkmval;
			int lastkmval;
			int fuelval;

			SetContentView (Resource.Layout.Main);

			var myButton = FindViewById<Button> (Resource.Id.button1);

			myButton.Click+= (sender, e) => {
				var second = new Intent(this, typeof(SecondActivity));
				second.PutExtra("FirstData", "Data from FirstActivity");
				StartActivity(second);
			};

			string message;
			bool resulttest = true;

			var testButton = new Button (this);
			testButton.Text = "travel to next Page";

			testButton.Click += (sender, e) => {
				var second = new Intent(this, typeof(SecondActivity));
				second.PutExtra("FirstData", "Data from FirstActivity");
				StartActivity (second);
			};

			var aLabel = new TextView (this);
			aLabel.Text = "Result:";
			aLabel.TextSize = 50;

		//	Toast.MakeText (this, aLabel.Text, ToastLength.Long).Show ();
			var currentkm = new TextView (this);
			currentkm.Text ="Odometer Current:";

			var currkm = new EditText (this);
			currkm.Text = Convert.ToString(45);
			currkmval = Convert.ToInt32(currkm.Text);

			currkm.AfterTextChanged += (sender, e) => {
				try {
				currkmval = Convert.ToInt32(currkm.Text);
				}
				catch(ArithmeticException exe)
				{

					message =exe.ToString();
					test(delegate(bool b) { },message);
				}
				catch(Exception ex)
				{
					ex.ToString();
				}
			};

			var previouskm = new TextView (this);
			previouskm.Text ="Odometer Previous:";

			var lastkm = new EditText (this);
			lastkm.Text = Convert.ToString(20);
			lastkmval = Convert.ToInt32(lastkm.Text);
			lastkm.AfterTextChanged += (sender, e) => {
				try{
				lastkmval = Convert.ToInt32(lastkm.Text);
				}
				catch(ArithmeticException exe)
				{

					message =exe.ToString();
					test(delegate(bool b) { },message);
				}
				catch(Exception ex)
				{
					ex.ToString();
				}
			};


			var refill = new TextView (this);
			refill.Text ="Refill Quantity:";


			var fuel = new EditText (this);
			fuel.Text = Convert.ToString(5);
			fuelval = Convert.ToInt32(fuel.Text);
			fuel.AfterTextChanged += (sender, e) => {
				try{
				fuelval = Convert.ToInt32(fuel.Text);
				}
				catch(Exception exe)
				{

					exe.GetBaseException();
				}

			


			};


			var aButton = new Button (this);
			aButton.Text = "Click to see the result";





			aButton.Click += (sender, e) => {
				resulttest =true;
				aLabel.Text = "Result:";
				int mileage;
				mileage = 0;
				if(currkmval<lastkmval)
				{ message = "Please enter current odometer value greater than previous odometer value:";
					resulttest=false;
					test(delegate(bool b) { },message);

				}

				if(currkmval<0 || lastkmval<0)
				{
					resulttest =false;
					message = "Please  enter only positive values";
					test(delegate(bool b) { },message);
				}



				try{
				mileage = (currkmval-lastkmval)/fuelval;
					//Mileage = [(Odometer Current - Odometer Previous)/ Refill Quantity]
					resulttest =true;
				}
				catch(DivideByZeroException exe)
				{
					resulttest=false;
					message ="please enter only non-zero numbers"; 
					test(delegate(bool b) { },message);
					exe.ToString();
				}
				if(mileage<0)
				{
					resulttest=false;

				}
				if(resulttest)
				aLabel.Text += Convert.ToString(mileage);


			};


			layout.AddView (currentkm);
			layout.AddView(currkm);
			layout.AddView (previouskm);
			layout.AddView (lastkm);
			layout.AddView (refill);
			layout.AddView (fuel);
			layout.AddView (aLabel);
			layout.AddView (aButton);


			SetContentView (Resource.Layout.Main);
		}
		public void test(Action<bool> callback,string message)
		{
			AlertDialog.Builder builder = new AlertDialog.Builder(this);
			builder.SetTitle(Android.Resource.String.DialogAlertTitle);
			builder.SetIcon(Android.Resource.Drawable.IcDialogAlert);
			builder.SetMessage(message);
			builder.SetPositiveButton("OK", (sender, e) => {callback(true);});

			builder.Show();
		}

	}
}


